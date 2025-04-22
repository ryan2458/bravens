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
    public class PlayerProjectile : Component, ICollisionObserver
    {
        private GameObjectManager GameObjectManager { get; }

        private float speed = 500.0f;
        private int projectileDamage = 5;
        private Animation animation;

        public PlayerProjectile(GameObject parent, Texture2D spriteSheet) : base(parent, nameof(PlayerProjectile))
        {
            GameObjectManager = parent.Core.GameObjectManager;
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
            GameObject projectileGameObject = GetGameObject();
            Transform transform = projectileGameObject.GetComponent<Transform>();
            transform.Translate(new Vector2(0.0f, -speed * (float)deltaTime.ElapsedGameTime.TotalSeconds));
            animation.Update(deltaTime);

            if (!GameBounds.IsGameObjectVisible(projectileGameObject))
            {
                GameObjectManager.Destroy(projectileGameObject);
            }
        }

        public void OnCollisionEnter(Collider collider)
        {
            if (collider.Tag == Enums.CollisionTag.Enemy)
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
