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

        public PlayerGun(GameObject parent) : base(parent, nameof(PlayerGun))
        {
            GameObjectManager = parent.Core.GameObjectManager;
        }

        public override void Update(GameTime deltaTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                CreateAndFireProjectile();
            }
        }

        private void CreateAndFireProjectile()
        {
            //Vector2 position = GetGameObject().GetComponent<Transform>().Position;
            //GameObject projectile = GameObjectManager.Create(new Vector2(position.X, position.Y));
            ////projectile.AddComponent<Transform>();
            ////projectile.AddComponent<Sprite>();
            ////projectile.AddComponent<Projectile>();

            Vector2 position = GetGameObject().GetComponent<Transform>().Position;
            GameObject projectile = GameObjectManager.Create($"PlayerAProjectile{projectileCount++}", GetGameObject(), "enemyAProjectile");
            projectile.AddComponent<Projectile>();
            Transform transform = projectile.GetComponent<Transform>();
            transform.Translate(position);
        }
    }
}
