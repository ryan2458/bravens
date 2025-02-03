using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class Sprite : Component
    {
        private GameCore gameCore;
        private SpriteBatch spriteBatch;
        private ContentManager content;

        public Texture2D SpriteTexture { get; set; }

        public Vector2 Position { get; set; } = Vector2.Zero;

        public Sprite(GameCore core, GameObject parent) : base(parent, nameof(Sprite))
        {
            gameCore = core;
            spriteBatch = new SpriteBatch(gameCore.GraphicsDevice);
            content = gameCore.Content;

            SpriteTexture = content.Load<Texture2D>("ball");
        }

        public override void Update(GameTime deltaTime)
        {
            Position = GetGameObject().GetComponent<Transform>().Position;
        }

        public override void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(SpriteTexture, Position, Color.White);
            spriteBatch.End();
        }
    }
}
