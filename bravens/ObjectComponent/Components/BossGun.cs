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

        // Burst firing variables
        private int burstSize = 10; // Number of shots in a burst
        private double timeBetweenShotsInBurst = 0.1; // Delay between shots inside a burst
        private double timeBetweenBursts = 3; // Delay before starting a new burst

        private double accumulatedTime = 0.0;
        private int shotsFiredInBurst = 0;
        private bool isBurstFiring = false;

        private static int projectileCount = 0;

        public BossGun(GameObject parent) : base(parent, nameof(BossGun))
        {
            GameObjectManager = parent.Core.GameObjectManager;
        }

        public override void Update(GameTime deltaTime)
        {
            accumulatedTime += deltaTime.ElapsedGameTime.TotalSeconds;

            if (isBurstFiring)
            {
                if (shotsFiredInBurst < burstSize && accumulatedTime >= timeBetweenShotsInBurst)
                {
                    CreateAndFireProjectile();
                    shotsFiredInBurst++;
                    accumulatedTime = 0;
                }

                if (shotsFiredInBurst >= burstSize)
                {
                    isBurstFiring = false;
                    shotsFiredInBurst = 0;
                    accumulatedTime = 0;
                }
            }
            else
            {
                if (accumulatedTime >= timeBetweenBursts)
                {
                    isBurstFiring = true;
                    accumulatedTime = 0;
                }
            }
        }

        private void CreateAndFireProjectile()
        {
            Vector2 position = GetGameObject().GetComponent<Transform>().Position;

            GameObject projectile1 = GameObjectManager.Create($"BossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile2 = GameObjectManager.Create($"BossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile3 = GameObjectManager.Create($"BossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");

            projectile1.AddComponent<BossProjectile>();
            projectile2.AddComponent<BossProjectile>();
            projectile3.AddComponent<BossProjectile>();

            projectile1.AddComponent<Collider>();
            projectile2.AddComponent<Collider>();
            projectile3.AddComponent<Collider>();

            projectile1.GetComponent<Collider>().Tag = Enums.CollisionTag.EnemyProjectile;
            projectile2.GetComponent<Collider>().Tag = Enums.CollisionTag.EnemyProjectile;
            projectile3.GetComponent<Collider>().Tag = Enums.CollisionTag.EnemyProjectile;

            Transform transform1 = projectile1.GetComponent<Transform>();
            Transform transform2 = projectile2.GetComponent<Transform>();
            Transform transform3 = projectile3.GetComponent<Transform>();

            transform1.Translate(position);
            transform2.Translate(position);
            transform3.Translate(position);
        }
    }
}