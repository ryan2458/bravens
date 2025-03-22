using bravens.ObjectComponent.Components;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.Utilities
{
    public class GameBounds
    {
        /// <summary>
        /// Checks if the game object is visible on screen.
        /// </summary>
        /// <param name="gameObject">
        /// The game object we wish to know is on screen or not.
        /// </param>
        /// <returns>
        /// <c>true</c> if the game object is visible on screen, <c>false</c> otherwise.
        /// </returns>
        public static bool IsGameObjectVisible(GameObject gameObject)
        {
            GraphicsDeviceManager graphics = gameObject.Core.GraphicsDeviceManager;
            Transform transform = gameObject.GetComponent<Transform>();
            Sprite sprite = gameObject.GetComponent<Sprite>();

            return !(transform.Position.Y > graphics.PreferredBackBufferHeight + sprite.SpriteTexture.Height) &&
                   !(transform.Position.Y < 0) &&
                   !(transform.Position.X > graphics.PreferredBackBufferWidth) &&
                   !(transform.Position.X < sprite.SpriteTexture.Width);
        }
    }
}
