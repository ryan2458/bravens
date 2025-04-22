using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


namespace bravens.ObjectComponent.Components
{
    public class EnemyBBehaviour : Component
    {
        private readonly Transform transform;

        private readonly Sprite sprite;
        //private readonly EnemyBGun gun;

        private Animation animation;

        private float speed = 30.0f;
        private int yDirection = 1;

        public EnemyBBehaviour(GameObject parent) : base(parent, nameof(EnemyABehaviour))
        {
            Random random = new Random();
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
            //gun = parent.GetComponent<EnemyBGun>();
            var spriteSheet = parent.Core.Content.Load<Texture2D>("enemy-medium");

            animation = new Animation(
            this,
            spriteSheet,
            frameWidth: 128,
            frameHeight: 64,
            frameCount: 2,
            frameTime: 0.2f
            );

            transform.SetPositionX(random.Next(sprite.SpriteTexture.Width / 2, graphics.PreferredBackBufferWidth - sprite.SpriteTexture.Width / 2));
        }

        public override void Update(GameTime deltaTime)
        {
            Vector2 movement = Vector2.Zero;
            animation.Update(deltaTime);
            movement.Y = 1;

            transform.Translate(movement * speed * (float)deltaTime.ElapsedGameTime.TotalSeconds);

            ResetHeight();
        }

        private void ResetHeight()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;
            if (transform.Position.Y > graphics.PreferredBackBufferHeight + sprite.SpriteTexture.Width / 2)
            {
                transform.SetPositionY(-1 * sprite.SpriteTexture.Height / 2);

                Random random = new Random();
                transform.SetPositionX(random.Next(sprite.SpriteTexture.Width / 2, graphics.PreferredBackBufferWidth - sprite.SpriteTexture.Width / 2));
            }
        }

        public override void Draw()
        {
            var spriteBatch = GetGameObject().Core.SpriteBatch;
            animation.Draw(spriteBatch, transform.Position);
        }
    }
}
