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
        /// <summary>
        /// Collision event handler raised when a collision occurs.
        /// </summary>
        /// <param name="collider">
        /// The collider that has been collided against.
        /// </param>
        void OnCollisionEnter(Collider collider);
    }
}
