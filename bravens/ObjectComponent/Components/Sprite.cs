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
        private SpriteBatch spriteBatch;
        private ContentManager content;

        public Texture2D SpriteTexture { get; set; }

        public Vector2 Position { get; set; } = Vector2.Zero;

        public float Rotation { get; set; }

        public Sprite(GameObject parent) : base(parent, nameof(Sprite))
        {
            spriteBatch = parent.Core.SpriteBatch;
            content = parent.Core.Content;
            
            SpriteTexture = content.Load<Texture2D>("balls");
        }

        public override void Update(GameTime deltaTime)
        {
            Position = GetGameObject().GetComponent<Transform>().Position;
            Rotation = GetGameObject().GetComponent<Transform>().Rotation;
        }

        public override void Draw()
        {
            Vector2 origin = new Vector2(SpriteTexture.Width / 2, SpriteTexture.Height / 2);
            spriteBatch.Begin();
            spriteBatch.Draw(SpriteTexture, Position, null, Color.White, Rotation,
            origin, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }


        /// <summary>
        /// Flips the X component of this sprite.
        /// </summary>
        public void FlipX()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Flips the Y component of this sprite.
        /// </summary>
        public void FlipY()
        {
            throw new NotImplementedException();
        }
    }
}
