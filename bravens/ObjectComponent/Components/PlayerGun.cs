using bravens.Managers;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class PlayerGun : Component
    {
        private int projectileCount = 0;

        private GameObjectManager GameObjectManager { get; }

        private double lastShotTime = 0;
        private double shotCooldown = 250; // Delay between shots in milliseconds

        public PlayerGun(GameObject parent) : base(parent, nameof(PlayerGun))
        {
            GameObjectManager = parent.Core.GameObjectManager;
        }

        public override void Update(GameTime deltaTime)
        {
            double currentTime = deltaTime.TotalGameTime.TotalMilliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.F) || Keyboard.GetState().IsKeyDown(Keys.Space)
                && currentTime - lastShotTime >= shotCooldown)
            {
                CreateAndFireProjectile();
                lastShotTime = currentTime; // Reset timer
            }
        }

        private void CreateAndFireProjectile()
        {
            Vector2 position = GetGameObject().GetComponent<Transform>().Position;
            GameObject projectile = GameObjectManager.Create($"PlayerAProjectile{projectileCount++}", GetGameObject(), "enemyAProjectile");
            projectile.AddComponent<PlayerProjectile>();
            projectile.AddComponent<Collider>();
            projectile.GetComponent<Collider>().Tag = Enums.CollisionTag.PlayerProjectile;
            Transform transform = projectile.GetComponent<Transform>();
            transform.Translate(position);
        }
    }
}
