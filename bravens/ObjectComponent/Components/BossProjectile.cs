using bravens.Managers;
using bravens.ObjectComponent.Interfaces;
using bravens.ObjectComponent.Objects;
using bravens.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class BossProjectile : Component, ICollisionObserver
    {
        private GameObjectManager GameObjectManager { get; }

        private readonly Transform transform;
        private readonly Sprite sprite;

        private readonly float yOffset = GetRandomYOffset();
        private float spreadOffsetX = 0f;

        public float speed { get; set; }
        public int projectileDamage { get; set; }
        private string movementType;

        public BossProjectile(GameObject parent, Texture2D sprite, string movementType, float spreadOffsetX = 0f) : base(parent, nameof(BossProjectile))
        {
            GameObjectManager = parent.Core.GameObjectManager;
            transform = parent.GetComponent<Transform>();
            this.sprite = parent.GetComponent<Sprite>();

            this.movementType = movementType;
            this.spreadOffsetX = spreadOffsetX;
        }

        public override void Update(GameTime deltaTime)
        {
            GameObject projectileGameObject = GetGameObject();
            Transform transform = projectileGameObject.GetComponent<Transform>();
            // transform.Translate(new Vector2(yOffset, speed * (float)deltaTime.ElapsedGameTime.TotalSeconds));

            Vector2 movement = GetMovement((float)deltaTime.ElapsedGameTime.TotalSeconds);
            transform.Translate(movement);

            if (!GameBounds.IsGameObjectVisible(projectileGameObject))
            {
                GameObjectManager.Destroy(projectileGameObject);
            }
        }

        private Vector2 GetMovement(float deltaSeconds)
        {
            float vx = spreadOffsetX; // Default horizontal spread

            switch (movementType.ToLower())
            {
                case "spiral":
                    float spiralSpeed = 50f;
                    vx += (float)Math.Cos(transform.Position.Y / 20) * spiralSpeed;
                    break;
                case "zigzag":
                    vx += (float)Math.Sin(transform.Position.Y / 30) * 50f;
                    break;
                case "curveleft":
                    vx += -30f * deltaSeconds;
                    break;
                case "curveright":
                    vx += 30f * deltaSeconds;
                    break;
                case "straight":
                default:
                    break;
            }

            return new Vector2(vx, speed * deltaSeconds);
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
        /// Generate a random number to indicate the angle which
        /// will be between -2.5 and 2.5.
        /// </summary>
        /// <returns>Random yOffset for current projectile.</returns>
        private static float GetRandomYOffset()
        {
            var random = new Random();

            return (float)(random.NextDouble() * 5 - 2.5);
        }

        /// <inheritdoc />
        public void OnCollisionEnter(Collider collider)
        {
            if (collider.Tag == Enums.CollisionTag.Player)
            {
                collider.GetGameObject().GetComponent<Health>().DamageUnit(projectileDamage);
                GameObjectManager.Destroy(GetGameObject());
            }
        }
    }
}
