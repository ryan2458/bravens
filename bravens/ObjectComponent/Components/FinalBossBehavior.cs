﻿using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class FinalBossBehavior : Component
    {
        private readonly Random _random = new();

        private readonly Transform transform;

        private readonly Sprite sprite;
        private readonly BossGun gun;

        private float speed = 200.0f;
        private int xDirection = 1;

        public FinalBossBehavior(GameObject parent) : base(parent, nameof(FinalBossBehavior))
        {
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
            gun = parent.GetComponent<BossGun>();

            KeepFromTopOfScreen();
            CenterBoss();
        }

        public override void Update(GameTime deltaTime)
        {
            Vector2 movement = Vector2.Zero;
            movement.X = xDirection;

            transform.Translate(movement * speed * (float)deltaTime.ElapsedGameTime.TotalSeconds);

            SwitchDirectionsIfNeeded();
        }

        private void SwitchDirectionsIfNeeded()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;

            var randMax = _random.Next(graphics.PreferredBackBufferWidth / 3);

            if (transform.Position.X > graphics.PreferredBackBufferWidth - sprite.SpriteTexture.Width / 2 - randMax)
            {
                xDirection = -1;
            }
            else if (transform.Position.X < sprite.SpriteTexture.Width / 2 + randMax)
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

        /// <summary>
        /// Center the boss on the screen.
        /// </summary>
        private void CenterBoss()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;

            transform.SetPositionX(graphics.PreferredBackBufferWidth / 2);
        }
    }
}

