using bravens.Managers;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class FinalBossGun : Component
    {
        private GameObjectManager GameObjectManager { get; }
        private double timeBetweenProjectileInSeconds = 0.5; // Faster in stage 1
        private double accumulatedTime = 0.0;

        public FinalBossGun(GameObject parent) : base(parent, nameof(FinalBossGun))
        {
            GameObjectManager = parent.Core.GameObjectManager;
        }

        public override void Update(GameTime deltaTime)
        {
            FinalBossBehavior bossBehavior = GetGameObject().GetComponent<FinalBossBehavior>();

            if (accumulatedTime >= timeBetweenProjectileInSeconds)
            {
                CreateAndFireProjectile(bossBehavior);
                accumulatedTime = 0.0;
            }
            else
            {
                accumulatedTime += deltaTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void CreateAndFireProjectile(FinalBossBehavior bossBehavior)
        {
            Vector2 position = GetGameObject().GetComponent<Transform>().Position;

            if (bossBehavior.CurrentStage == 1)
            {
                // Stage 1: Fire a single projectile
                GameObject projectile = GameObjectManager.Create("FinalBossProjectile", GetGameObject(), "finalBossProjectile");
                projectile.AddComponent<FinalBossProjectile>();
                Transform transform = projectile.GetComponent<Transform>();
                transform.Translate(position);
            }
            else if (bossBehavior.CurrentStage == 2)
            {
                // Stage 2: Fire multiple projectiles in a spread pattern
                FireSpreadProjectiles(position);
            }
        }

        private void FireSpreadProjectiles(Vector2 position)
        {
            // Fire three projectiles in a spread pattern
            for (int i = -1; i <= 1; i++)
            {
                GameObject projectile = GameObjectManager.Create($"FinalBossProjectileSpread{i}", GetGameObject(), "finalBossProjectile");
                projectile.AddComponent<FinalBossProjectile>();
                Transform transform = projectile.GetComponent<Transform>();
                transform.Translate(position);

                // Adjust the projectile's direction based on the spread
                FinalBossProjectile finalBossProjectile = projectile.GetComponent<FinalBossProjectile>();
                finalBossProjectile.SetDirection(new Vector2(i * 50, 300)); // Adjust the spread angle and speed
            }
        }
    }
}