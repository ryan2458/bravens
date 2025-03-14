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
    public class MidBossProjectile : Component
    {
        private GameObjectManager GameObjectManager { get; }
        private readonly Transform transform;
        private readonly Sprite sprite;

        private readonly float speed = 250.0f; // Speed of the projectile
        private Vector2 direction;

        public MidBossProjectile(GameObject parent) : base(parent, nameof(MidBossProjectile))
        {
            GameObjectManager = parent.Core.GameObjectManager;
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
        }

        public override void Update(GameTime deltaTime)
        {
            transform.Translate(direction * speed * (float)deltaTime.ElapsedGameTime.TotalSeconds);

            if (!IsVisible())
            {
                GameObjectManager.Destroy(GetGameObject());
            }
        }

        public void SetDirection(Vector2 newDirection)
        {
            direction = Vector2.Normalize(newDirection); // Normalize to ensure consistent speed
        }

        private bool IsVisible()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;

            return !(transform.Position.Y > graphics.PreferredBackBufferHeight + sprite.SpriteTexture.Height) &&
                   !(transform.Position.Y < 0) &&
                   !(transform.Position.X > graphics.PreferredBackBufferWidth) &&
                   !(transform.Position.X < sprite.SpriteTexture.Width);
        }
    }
}
