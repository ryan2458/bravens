using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent
{
    public abstract class BaseObject
    {
        public BaseObject Parent { get; set; }

        public string Name { get; set; }

        public BaseObject(string name)
        {
            Parent = null;
            Name = name;
        }

        /* Implement in deriving concrete classes as necessary.  Do not add implementation logic here */
        public virtual void Initialize() { }
        public virtual void Load() { }
        public virtual void Unload() { }
        public virtual void Update(GameTime deltaTime) { }
        public virtual void Draw() { }

    }
}
