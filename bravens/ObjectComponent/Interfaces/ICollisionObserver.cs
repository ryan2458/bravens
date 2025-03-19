using bravens.ObjectComponent.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Interfaces
{
    internal interface ICollisionObserver
    {
        void OnCollisionEnter(Collider collider);
    }
}
