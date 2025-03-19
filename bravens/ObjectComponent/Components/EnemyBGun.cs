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
    public class EnemyBGun : Component
    {
        private GameObjectManager GameObjectManager { get; }

        private double timeBetweenProjectileInSeconds = 1.5;

        private int projectileCount = 0;
        private double accumulatedTime = 0.0;

        public EnemyBGun(GameObject parent) : base(parent, nameof(EnemyAGun))
        {
            GameObjectManager = parent.Core.GameObjectManager;
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
            GameObject projectile = GameObjectManager.Create($"EnemyBProjectile{projectileCount}", GetGameObject(), "enemyAProjectile");
            projectile.AddComponent<EnemyBProjectile>();
            Transform transform = projectile.GetComponent<Transform>();
            transform.Translate(position);
            projectileCount++;
        }
    }
}
