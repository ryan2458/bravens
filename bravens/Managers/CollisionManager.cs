using bravens.ObjectComponent;
using bravens.ObjectComponent.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.Managers
{
    public class CollisionManager : BaseObject
    {
        private static readonly List<Collider> colliders = new List<Collider>();

        public CollisionManager() : base(nameof(CollisionManager))
        {
        }

        public static void RegisterCollider(Collider collider)
        {
            colliders.Add(collider);
        }

        public static void UnregisterCollider(Collider collider)
        {
            colliders.Remove(collider);
        }

        public static void CheckCollisions()
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                for (int j = i + 1; j < colliders.Count; j++)
                {
                    Collider a = colliders[i];
                    Collider b = colliders[j];
                    if (IsColliding(a, b))
                    {
                        a.TriggerCollisionEnter(b);
                        b.TriggerCollisionEnter(a);
                    }
                }
            }
        }

        private static bool IsColliding(Collider a, Collider b)
        {
            return Vector2.Distance(a.Position, b.Position) < (a.Radius + b.Radius);
        }
    }
}
