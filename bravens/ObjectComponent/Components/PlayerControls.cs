using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace bravens.ObjectComponent.Components
{
    public class PlayerControls : Component
    {
        private readonly Transform transform;

        private readonly Sprite sprite;

        private float currentSpeed;
        private float normalSpeed = 400.0f;
        private float slowedSpeed = 200.0f;

        private float rotationSpeed = 25.0f;


        public PlayerControls(GameObject parent) : base(parent, nameof(PlayerControls))
        {
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
        }

        public override void Update(GameTime deltaTime)
        {
            currentSpeed = Keyboard.GetState().IsKeyDown(Keys.Space) ? slowedSpeed : normalSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                transform.Translate(new Vector2(0, -currentSpeed * (float)deltaTime.ElapsedGameTime.TotalSeconds));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                transform.Translate(new Vector2(-currentSpeed * (float)deltaTime.ElapsedGameTime.TotalSeconds, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                transform.Translate(new Vector2(0, currentSpeed * (float)deltaTime.ElapsedGameTime.TotalSeconds));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                transform.Translate(new Vector2(currentSpeed * (float)deltaTime.ElapsedGameTime.TotalSeconds, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                transform.Rotate(rotationSpeed * (float)deltaTime.ElapsedGameTime.TotalSeconds);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                transform.Rotate(-rotationSpeed * (float)deltaTime.ElapsedGameTime.TotalSeconds);
            }

            ConfineToWindow();
        }

        /// <summary>
        /// Confines movement to inside the game window.
        /// </summary>
        private void ConfineToWindow()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;

            if (transform.Position.X > graphics.PreferredBackBufferWidth - sprite.SpriteTexture.Width / 2)
            {
                transform.SetPositionX(graphics.PreferredBackBufferWidth - sprite.SpriteTexture.Width / 2);
            }
            else if (transform.Position.X < sprite.SpriteTexture.Width / 2)
            {
                transform.SetPositionX(sprite.SpriteTexture.Width / 2);
            }

            if (transform.Position.Y > graphics.PreferredBackBufferHeight - sprite.SpriteTexture.Height / 2)
            {
                transform.SetPositionY(graphics.PreferredBackBufferHeight - sprite.SpriteTexture.Height / 2);
            }
            else if (transform.Position.Y < sprite.SpriteTexture.Height / 2)
            {
                transform.SetPositionY(sprite.SpriteTexture.Height / 2);
            }
        }
    }
}
