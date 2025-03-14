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
    public class MidBossGun : Component
    {
        private GameObjectManager GameObjectManager { get; }
        private double timeBetweenProjectileInSeconds = 1.0; // Slower firing rate
        private double accumulatedTime = 0.0;

        public MidBossGun(GameObject parent) : base(parent, nameof(MidBossGun))
        {
            GameObjectManager = parent.Core.GameObjectManager;
        }

        public override void Update(GameTime deltaTime)
        {
            if (accumulatedTime >= timeBetweenProjectileInSeconds)
            {
                CreateAndFireProjectile();
                accumulatedTime = 0.0;
            }
            else
            {
                accumulatedTime += deltaTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void CreateAndFireProjectile()
        {
            Vector2 position = GetGameObject().GetComponent<Transform>().Position;
            GameObject projectile = GameObjectManager.Create("MidBossProjectile", GetGameObject(), "midBossProjectile");
            projectile.AddComponent<MidBossProjectile>();
            Transform transform = projectile.GetComponent<Transform>();
            transform.Translate(position);

            MidBossProjectile midBossProjectile = projectile.GetComponent<MidBossProjectile>();
            midBossProjectile.SetDirection(new Vector2(0, 300)); // Fire straight down

            // Optionally, add a spread pattern
            FireSpreadProjectiles(position);
        }

        private void FireSpreadProjectiles(Vector2 position)
        {
            for (int i = -1; i <= 1; i++)
            {
                GameObject projectile = GameObjectManager.Create($"MidBossProjectileSpread{i}", GetGameObject(), "midBossProjectile");
                projectile.AddComponent<MidBossProjectile>();
                Transform transform = projectile.GetComponent<Transform>();
                transform.Translate(position);

                MidBossProjectile midBossProjectile = projectile.GetComponent<MidBossProjectile>();
                midBossProjectile.SetDirection(new Vector2(i * 50, 300)); // Adjust the spread angle and speed
            }
        }
    }
}
