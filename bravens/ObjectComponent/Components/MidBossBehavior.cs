using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class MidBossBehavior : Component
    {
        private readonly Transform transform;

        private readonly Sprite sprite;
        //private readonly EnemyBGun gun;

        private float speed = 500.0f;
        private int xDirection = 1;

        private double timeBetweenDirectionSwitches = 1.0;
        private double accumulatedTime = 0.0;

        public MidBossBehavior(GameObject parent) : base(parent, nameof(EnemyABehaviour))
        {
            Random random = new Random();
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
            //gun = parent.GetComponent<EnemyBGun>();

            transform.SetPositionX(random.Next(sprite.SpriteTexture.Width / 2, graphics.PreferredBackBufferWidth - sprite.SpriteTexture.Width / 2));
        }

        public override void Update(GameTime deltaTime)
        {
            Vector2 movement = Vector2.Zero;
            movement.X = xDirection;

            transform.Translate(movement * speed * (float)deltaTime.ElapsedGameTime.TotalSeconds);

            accumulatedTime += deltaTime.ElapsedGameTime.TotalSeconds;

            SwitchDirections(deltaTime);
            KeepFromTopOfScreen();
        }

        private void SwitchDirections(GameTime deltaTime)
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;
            if (accumulatedTime + deltaTime.ElapsedGameTime.TotalSeconds >= timeBetweenDirectionSwitches)
            {
                Random random = new Random();
                int newDirection = random.Next(0, 2);
                if(newDirection == 0)
                {
                    xDirection = -1;
                }
                else
                {
                    xDirection = 1;
                }

                accumulatedTime = 0f;
            }

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
