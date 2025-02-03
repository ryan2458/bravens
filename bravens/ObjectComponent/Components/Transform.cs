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

        public Vector2 Position { get; set; }

        public Vector2 Scalar { get; set; }

        public float Rotation { get; set; }

        public Transform(GameObject parent) : base(parent, nameof(Transform))
        {
            transform = Matrix.Identity;
            Position = Vector2.Zero;
        }

        public void Translate(Vector2 vector)
        {
            Position += vector;
            // is a transform even necessary?
            transform.Translation = new Vector3(Position.X, Position.Y, 0.0f);
        }

        public void Rotate(float angle)
        {
            Rotation += angle;
        }

        public void Scale(Vector2 scalar)
        {
            Scalar += scalar;
        }
    }
}
