using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Objects
{
    public class GameObject : BaseObject
    {
        public List<Component> Components { get; } = [];

        public GameObject(string name) : base(name)
        {
        }

        public override void Draw()
        {
            for (int i = 0; i < Components.Count; ++i)
            {
                Components[i].Draw();
            }
        }

        public override void Update(GameTime deltaTime)
        {
            for (int i = 0; i < Components.Count; ++i)
            {
                Components[i].Update(deltaTime);
            }
        }
    }
}
