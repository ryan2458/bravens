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
    public class EnemyAGun : Component
    {
        private GameObjectManager GameObjectManager { get; }

        private double timeBetweenProjectileInSeconds = 1.0;
        private double currentTimeBetweenProjectileInSeconds = 0.0f;
        private int fasterFireIndex = 5; // Every n projectiles will come out a little quicker. Ex. 5, every fifth bullet will come out half a cooldown faster

        private int projectileCount = 0;
        private double accumulatedTime = 0.0;

        public EnemyAGun(GameObject parent) : base(parent, nameof(EnemyAGun)) 
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
            GameObject projectile = GameObjectManager.Create($"EnemyAProjectile{projectileCount}" , GetGameObject(), "enemyAProjectile");
            projectile.AddComponent<EnemyAProjectile>();
            Transform transform = projectile.GetComponent<Transform>();
            transform.Translate(position);
            projectileCount++;
        }
    }
}
