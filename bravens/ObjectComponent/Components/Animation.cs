using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;

namespace bravens.ObjectComponent.Components
{
    class Animation : Component
    {
        private Texture2D spriteSheet;
        private int frameWidth;
        private int frameHeight;
        private int frameCount;
        private float frameTime;
        private float elapsedTime;
        private int currentFrame;

        public Animation(BaseObject parent, Texture2D spriteSheet, int frameWidth, int frameHeight, int frameCount, float frameTime)
            : base(parent, "Animation")
        {
            this.spriteSheet = spriteSheet;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.elapsedTime = 0f;
            this.currentFrame = 0;
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= frameTime)
            {
                currentFrame = (currentFrame + 1) % frameCount;
                elapsedTime = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (spriteSheet == null) return;

            var sourceRectangle = new Rectangle(
                currentFrame * frameWidth, // X position of the frame
                0,                        // Y position
                frameWidth,               // Width of the frame
                frameHeight               // Height of the frame
            );

            spriteBatch.Draw(spriteSheet, position, sourceRectangle, Color.White);
        }
    }
}
