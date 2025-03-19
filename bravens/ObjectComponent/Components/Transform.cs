using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;

namespace bravens.ObjectComponent.Components
{
    /// <summary>
    /// Represents a transform component.
    /// Transforms give GameObjects a location in the space.
    /// </summary>
    public class Transform : Component
    {
        /// <summary>
        /// Gets the transform's current position.
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// Gets the transform's current scalar value.
        /// </summary>
        public Vector2 Scalar { get; private set; }

        /// <summary>
        /// Gets the transform's current rotation angle.
        /// </summary>
        public float Rotation { get; private set; }

        /// <summary>
        /// Gets the transform's current velocity.
        /// </summary>
        public Vector2 Velocity { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="Transform"/> component.
        /// </summary>
        /// <param name="parent">The game object this component is attached to.</param>
        public Transform(GameObject parent) : base(parent, nameof(Transform))
        {
            Position = Vector2.Zero;
        }

        /// <summary>
        /// Translate this transform's position by <paramref name="vector"/>
        /// </summary>
        /// <param name="vector">The vector to translate this transform by.</param>
        public void Translate(Vector2 vector)
        {
            Position += vector;
        }

        public void SetPositionX(float xPos)
        {
            Position = new Vector2(xPos, Position.Y);
        }

        public void SetPositionY(float yPos)
        {
            Position = new Vector2(Position.X, yPos);
        }

        public void SetPositionXY(float xPos, float yPos)
        {
            SetPositionX(xPos);
            SetPositionY(yPos);
        }

        /// <summary>
        /// Rotates this transform.
        /// </summary>
        /// <param name="angle">The angle (in degrees) to rotate this transform by.</param>
        /// <remarks>
        /// Positive values rotate clockwise.
        /// Negative values rotate counterclockwise.
        /// </remarks>
        public void Rotate(float angle)
        {
            Rotation += angle;
        }

        /// <summary>
        /// Scales this transform.
        /// </summary>
        /// <param name="scalar">The value to scale this transform by.</param>
        public void Scale(Vector2 scalar)
        {
            Scalar += scalar;
        }
    }
}
