using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class EnemyABehaviour : Component
    {
        private readonly Transform transform;

        private readonly Sprite sprite;
        private readonly EnemyAGun gun;

        private float speed = 200.0f;
        private int xDirection = 1;

        public EnemyABehaviour(GameObject parent) : base(parent, nameof(EnemyABehaviour))
        {
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
            gun = parent.GetComponent<EnemyAGun>();
        }

        public override void Update(GameTime deltaTime)
        {
            Vector2 movement = Vector2.Zero;
            movement.X = xDirection;

            transform.Translate(movement * speed * (float)deltaTime.ElapsedGameTime.TotalSeconds);

            SwitchDirections();
            KeepFromTopOfScreen();
        }

        private void SwitchDirections()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;

            if (transform.Position.X > graphics.PreferredBackBufferWidth - sprite.SpriteTexture.Width / 2)
            {
                xDirection = -1;
            }
            else if (transform.Position.X < sprite.SpriteTexture.Width / 2)
            {
                xDirection = 1;
            }
        }

        private void KeepFromTopOfScreen() 
        {
            if (transform.Position.Y < sprite.SpriteTexture.Height / 2)
            {
                transform.SetPositionY(sprite.SpriteTexture.Height / 2);
            }
        }
    }  
}
