using bravens.Managers;
using bravens.ObjectComponent.Enums;
using bravens.ObjectComponent.Interfaces;
using bravens.ObjectComponent.Objects;
using bravens.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace bravens.ObjectComponent.Components
{
    public class EnemyBProjectile : Component, ICollisionObserver
    {
        private GameObjectManager GameObjectManager { get; }

        private readonly Transform transform;
        private readonly Sprite sprite;
        private Animation animation;

        public float speed { get; set; }
        public int projectileDamage { get; set; }
        private string movementType;

        public EnemyBProjectile(GameObject parent, Texture2D spriteSheet, string movementType) : base(parent, nameof(EnemyBProjectile))
        {
            GameObjectManager = parent.Core.GameObjectManager;
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
            this.movementType = movementType;

            animation = new Animation(
                this,
                spriteSheet,
                frameWidth: 32,
                frameHeight: 64,
                frameCount: 2,
                frameTime: 0.2f);
        }

        public override void Update(GameTime deltaTime)
        {
            animation.Update(deltaTime);

            GameObject projectileGameObject = GetGameObject();
            Transform transform = projectileGameObject.GetComponent<Transform>();
            // transform.Translate(new Vector2(0.0f, speed * (float)deltaTime.ElapsedGameTime.TotalSeconds));

            Vector2 movement = GetMovement((float)deltaTime.ElapsedGameTime.TotalSeconds);
            transform.Translate(movement);

            if (!GameBounds.IsGameObjectVisible(projectileGameObject))
            {
                GameObjectManager.Destroy(projectileGameObject);
            }

            speed = speed + 5;
        }

        private Vector2 GetMovement(float deltaSeconds)
        {
            switch (movementType.ToLower())
            {
                case "spiral":
                    float spiralSpeed = 50f; // Adjust this as needed
                    return new Vector2((float)Math.Cos(transform.Position.Y / 20) * spiralSpeed, speed * deltaSeconds);
                case "zigzag":
                    return new Vector2((float)Math.Sin(transform.Position.Y / 30) * 50f, speed * deltaSeconds);
                case "curveleft":
                    return new Vector2(-30f * deltaSeconds, speed * deltaSeconds);
                case "curveright":
                    return new Vector2(30f * deltaSeconds, speed * deltaSeconds);
                case "straight":
                default:
                    return new Vector2(0f, speed * deltaSeconds);
            }
        }

        public void OnCollisionEnter(Collider collider)
        {
            if (collider.Tag == CollisionTag.Player)
            {
                collider.GetGameObject().GetComponent<Health>().DamageUnit(projectileDamage);
                GameObjectManager.Destroy(GetGameObject());
            }
        }

        public override void Draw()
        {
            var transform = GetGameObject().GetComponent<Transform>();
            var spriteBatch = GetGameObject().Core.SpriteBatch;

            animation.Draw(spriteBatch, transform.Position);
        }
    }
}
