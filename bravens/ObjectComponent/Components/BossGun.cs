using bravens.Managers;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public double timeBetweenShotsInBurst{ get; set; }
        private double timeBetweenBursts = 3; // Delay before starting a new burst

        private double accumulatedTime = 0.0;
        private int shotsFiredInBurst = 0;
        private bool isBurstFiring = false;

        private static int projectileCount = 0;
        private float speed;
        private int projectileDamage;
        private Texture2D projectileSprite;
        private string movementType;
        private List<float> spreadOffsets;

        public BossGun(GameObject parent, float fireInterval, float speed, int projectileDamage, Texture2D projectileSprite, string MovementType, ProjectileConfig config) : base(parent, nameof(BossGun))
        {
            GameObjectManager = parent.Core.GameObjectManager;
            this.timeBetweenBursts = fireInterval;
            this.speed = speed;
            this.projectileDamage = projectileDamage;
            this.projectileSprite = projectileSprite;
            this.movementType = MovementType;
            this.spreadOffsets = config.spreadOffsets;

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
            var spriteSheet = GetGameObject().Core.Content.Load<Texture2D>("BossProjectile");
            Vector2 position = GetGameObject().GetComponent<Transform>().Position;

            foreach (float spread in spreadOffsets)
            {
                GameObject projectile = GameObjectManager.Create($"BossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");

                var projectileComponent = projectile.AddComponent(() =>
                    new BossProjectile(projectile, projectileSprite, movementType, spread));

                projectileComponent.speed = speed;
                projectileComponent.projectileDamage = projectileDamage;

                projectile.AddComponent<Collider>();
                projectile.GetComponent<Collider>().Tag = Enums.CollisionTag.EnemyProjectile;

                projectile.GetComponent<Transform>().Translate(position);
            }
        }
    }
}