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
    public class BossProjectile : Component
    {
        private GameObjectManager GameObjectManager { get; }

        private readonly Transform transform;
        private readonly Sprite sprite;

        private readonly float speed = 500.0f + new Random().Next(-5, 3);

        private readonly float yOffset = GetRandomYOffset();

        public BossProjectile(GameObject parent) : base(parent, nameof(BossProjectile))
        {
            GameObjectManager = parent.Core.gameObjectManager;
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
        }

        public override void Update(GameTime deltaTime)
        {
            GameObject projectileGameObject = GetGameObject();

            Transform transform = projectileGameObject.GetComponent<Transform>();

            transform.Translate(new Vector2(yOffset, speed * (float)deltaTime.ElapsedGameTime.TotalSeconds));

            if (!IsVisible())
            {
                GameObjectManager.Destroy(GetGameObject());
            }
        }

        /// <summary>
        /// Checks if the projectile is visible on the screen. This will check for all bounds.
        /// </summary>
        /// <returns>Indication if the projectile is visible.</returns>
        private bool IsVisible()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;

            return !(transform.Position.Y > graphics.PreferredBackBufferHeight + sprite.SpriteTexture.Height) &&
                   !(transform.Position.Y < 0) &&
                   !(transform.Position.X > graphics.PreferredBackBufferWidth) &&
                   !(transform.Position.X < sprite.SpriteTexture.Width);
        }

        /// <summary>
        ///  Generate a random number to indicate the angle which
        /// will be between -2.5 and 2.5.
        /// </summary>
        /// <returns>Random yOffset for current projectile.</returns>
        private static float GetRandomYOffset()
        {
            var random = new Random();

            return (float)(random.NextDouble() * 5 - 2.5);
        }
    }
}
