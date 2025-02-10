using bravens.Managers;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class BossGun : Component
    {
        private GameObjectManager GameObjectManager { get; }

        private double timeBetweenProjectileInSeconds = 0.5;
        private double currentTimeBetweenProjectileInSeconds = 0.0f;
        private int fasterFireIndex = 1; // Every n projectiles will come out a little quicker. Ex. 5, every fifth bullet will come out half a cooldown faster

        private int projectileCount = 0;
        private double accumulatedTime = 0.0;

        public BossGun(GameObject parent) : base(parent, nameof(BossGun))
        {
            GameObjectManager = parent.Core.gameObjectManager;
        }

        public override void Update(GameTime deltaTime)
        {
            if (accumulatedTime + deltaTime.ElapsedGameTime.TotalSeconds >= currentTimeBetweenProjectileInSeconds)
            {
                CreateAndFireProjectile();

                currentTimeBetweenProjectileInSeconds = projectileCount % fasterFireIndex == 0 ? timeBetweenProjectileInSeconds / 2 : timeBetweenProjectileInSeconds;

                accumulatedTime = 0f;
            }
            else
            {
                accumulatedTime += deltaTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void CreateAndFireProjectile()
        {
            Vector2 position = GetGameObject().GetComponent<Transform>().Position;

            GameObject projectile1 = GameObjectManager.Create($"BossProjectile{projectileCount}", GetGameObject(), "bossProjectile");
            GameObject projectile2 = GameObjectManager.Create($"BossProjectile{projectileCount + 1}", GetGameObject(), "bossProjectile");
            GameObject projectile3 = GameObjectManager.Create($"BossProjectile{projectileCount + 2}", GetGameObject(), "bossProjectile");

            projectile1.AddComponent<BossProjectile>();
            projectile2.AddComponent<BossProjectile>();
            projectile3.AddComponent<BossProjectile>();

            Transform transform1 = projectile1.GetComponent<Transform>();
            Transform transform2 = projectile2.GetComponent<Transform>();
            Transform transform3 = projectile3.GetComponent<Transform>();

            transform1.Translate(position);
            transform2.Translate(position);
            transform3.Translate(position);

            projectileCount += 3;
        }
    }
}
