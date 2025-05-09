﻿using bravens.Managers;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class PlayerGun : Component
    {
        private int projectileCount = 0;

        private GameObjectManager GameObjectManager { get; }

        private double lastShotTime = 0;
        private double shotCooldown = 250; // Delay between shots in milliseconds
        private GameCore gameCore;

        public PlayerGun(GameObject parent) : base(parent, nameof(PlayerGun))
        {
            GameObjectManager = parent.Core.GameObjectManager;
            this.gameCore = parent.Core;
        }

        public override void Update(GameTime deltaTime)
        {
            double currentTime = deltaTime.TotalGameTime.TotalMilliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Space)
                && currentTime - lastShotTime >= shotCooldown)
            {
                CreateAndFireProjectile();
                lastShotTime = currentTime; // Reset timer
            }
        }

        private void CreateAndFireProjectile()
        {
            var spriteSheet = GetGameObject().Core.Content.Load<Texture2D>("PlayerProjectile");
            Vector2 playerPosition = GetGameObject().GetComponent<Transform>().Position;

            var playerSprite = GetGameObject().GetComponent<Sprite>();
            float playerWidth = playerSprite.SpriteTexture.Width;
            float playerHeight = playerSprite.SpriteTexture.Height;

            float projectileWidth = spriteSheet.Width;
            float projectileHeight = spriteSheet.Height;

            Vector2 projectilePosition = new Vector2(
                playerPosition.X - (playerWidth / 4) + (projectileWidth / 4),
                playerPosition.Y - (playerHeight / 2) + (projectileHeight / 2)
            );

            GameObject projectile = GameObjectManager.Create($"PlayerProjectile{projectileCount++}", GetGameObject(), "blank");

            projectile.AddComponent(() => new PlayerProjectile(projectile, spriteSheet));
            projectile.AddComponent<Collider>();


            Transform transform = projectile.GetComponent<Transform>();
            transform.Translate(projectilePosition);
        }

    }
}
