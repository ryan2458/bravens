using bravens.ObjectComponent.Objects;
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
        private Texture2D spriteTexture;

        public Sprite(GameObject parent) : base(parent, nameof(Sprite))
        {
        }

        public override void Draw()
        {
            
        }
    }
}
