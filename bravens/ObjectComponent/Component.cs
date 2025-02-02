using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent
{
    public abstract class Component : BaseObject
    {
        public Component(BaseObject parent, string name) : base(name)
        {
            if (parent == null)
            {
                throw new Exception("A component must have an object that it's attached to!");
            }

            Parent = parent;
        }
    }
}
