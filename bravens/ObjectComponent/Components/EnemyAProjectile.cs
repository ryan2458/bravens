using bravens.Managers;
using bravens.ObjectComponent.Enums;
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
    public class EnemyAProjectile : Component, ICollisionObserver
    {
        private GameObjectManager GameObjectManager { get; }

        private readonly Transform transform;
        private readonly Sprite sprite;
        private Animation animation;

        public float speed { get; set; }
        public int projectileDamage { get; set; }

        public EnemyAProjectile(GameObject parent, Texture2D spriteSheet) : base(parent, nameof(EnemyAProjectile))
        {
            GameObjectManager = parent.Core.GameObjectManager;
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();

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

            transform.Translate(new Vector2(0.0f, speed * (float)deltaTime.ElapsedGameTime.TotalSeconds));

            if (!GameBounds.IsGameObjectVisible(projectileGameObject)) 
            {
                GameObjectManager.Destroy(projectileGameObject);
            }
            // speed = speed + 5;
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
