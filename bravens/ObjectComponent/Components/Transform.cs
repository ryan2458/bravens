using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    internal class Transform : Component
    {
        private Matrix transform;

        public Vector3 Position { get; set; }

        public Vector3 Scalar { get; set; }

        public float Rotation { get; set; }

        public Transform(GameObject parent) : base(parent, nameof(Transform))
        {
            transform = Matrix.Identity;
        }

        public void Translate(Vector3 vector)
        {
            Position += vector;
            transform.Translation = new Vector3(Position.X, Position.Y, Position.Z);
        }

        public void Rotate(float angle)
        {
            Rotation += angle;
        }

        public void Scale(Vector3 scalar)
        {
            Scalar += scalar;
        }
    }
}
