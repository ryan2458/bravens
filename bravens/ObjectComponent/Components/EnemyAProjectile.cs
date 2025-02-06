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
    public class EnemyAProjectile : Component
    {
        private GameObjectManager GameObjectManager { get; }

        private readonly Transform transform;
        private readonly Sprite sprite;

        private float speed = 300.0f;

        public EnemyAProjectile(GameObject parent) : base(parent, nameof(EnemyAProjectile))
        {
            GameObjectManager = parent.Core.gameObjectManager;
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
        }

        public override void Update(GameTime deltaTime)
        {
            GameObject projectileGameObject = GetGameObject();

            Transform transform = projectileGameObject.GetComponent<Transform>();

            transform.Translate(new Vector2(0.0f, speed * (float)deltaTime.ElapsedGameTime.TotalSeconds));

            if (!IsVisible()) 
            {
                GameObjectManager.Destroy(GetGameObject());
            }
        }

        private bool IsVisible() 
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;

            if (transform.Position.Y > graphics.PreferredBackBufferHeight + sprite.SpriteTexture.Height)
            {
                return false;
            }

            return true;
        }

    }
}
