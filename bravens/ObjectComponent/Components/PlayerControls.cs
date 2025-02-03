using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class PlayerControls : Component
    {
        private Transform transform;

        public PlayerControls(GameObject parent) : base(parent, nameof(PlayerControls))
        {
            transform = parent.GetComponent<Transform>();
        }

        public override void Update(GameTime deltaTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                transform.Translate(new Vector2(0, -1.0f));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                transform.Translate(new Vector2(-1.0f, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                transform.Translate(new Vector2(0, 1.0f));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                transform.Translate(new Vector2(1.0f, 0));
            }
        }
    }
}
