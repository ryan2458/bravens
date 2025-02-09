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

        private double timeBetweenProjectileInSeconds = 0.8;

        private int projectileCount = 0;
        private double accumulatedTime = 0.0;

        private int projectileSpreadDistance = 50;

        public MidBossGun(GameObject parent) : base(parent, nameof(EnemyAGun))
        {
            GameObjectManager = parent.Core.gameObjectManager;
        }

        public override void Update(GameTime deltaTime)
        {
            if (accumulatedTime + deltaTime.ElapsedGameTime.TotalSeconds >= timeBetweenProjectileInSeconds)
            {
                CreateAndFireProjectile();

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
            GameObject projectile1 = GameObjectManager.Create($"MidBossProjectile{projectileCount}", GetGameObject(), "enemyAProjectile");
            projectile1.AddComponent<MidBossProjectile>();
            Transform transform1 = projectile1.GetComponent<Transform>();
            transform1.Translate(position);
            projectileCount++;

            GameObject projectile2 = GameObjectManager.Create($"MidBossProjectile{projectileCount}", GetGameObject(), "enemyAProjectile");
            projectile2.AddComponent<MidBossProjectile>();
            Transform transform2 = projectile2.GetComponent<Transform>();
            position.X = position.X + projectileSpreadDistance;
            transform2.Translate(position);
            projectileCount++;

            GameObject projectile3 = GameObjectManager.Create($"MidBossProjectile{projectileCount}", GetGameObject(), "enemyAProjectile");
            projectile3.AddComponent<MidBossProjectile>();
            Transform transform3 = projectile3.GetComponent<Transform>();
            position.X = position.X - projectileSpreadDistance * 2;
            transform3.Translate(position);
            projectileCount++;
        }
    }
}
