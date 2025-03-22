using bravens.Managers;
using bravens.ObjectComponent.Enums;
using bravens.ObjectComponent.Interfaces;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    /// <summary>
    /// A circle collider used for handling simple 2D collisions in the game world.
    /// </summary>
    public class Collider : Component
    {
        private Transform Transform { get; }

        /// <summary>
        /// Gets the position of the collider.
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// Gets or sets the radius of the collider.
        /// </summary>
        public float Radius { get; set; } = 25.0f;

        public CollisionTag Tag { get; set; } = CollisionTag.None;

        /// <summary>
        /// Initializes a new instance of <see cref="Collider"/>
        /// </summary>
        /// <param name="parent">
        /// The GameObject instance the collider is attached to.
        /// </param>
        public Collider(GameObject parent) : base(parent, nameof(Collider))
        {
            Sprite sprite = parent.GetComponent<Sprite>();

            Transform = parent.GetComponent<Transform>();
            Position = Transform.Position;

            // get an approximate radius.  We'll use sprites that have roughly even heights and widths.
            Radius = Math.Min(sprite.SpriteTexture.Height, sprite.SpriteTexture.Width) / 2;
            CollisionManager.RegisterCollider(this);
        }

        public override void Update(GameTime deltaTime)
        {
            Position = Transform.Position;
        }

        public override void Unload()
        {
            CollisionManager.UnregisterCollider(this);
        }

        public void TriggerCollisionEnter(Collider collision)
        {
            IEnumerable<ICollisionObserver> observers = GetGameObject().GetComponents().Where(c => c is ICollisionObserver).Cast<ICollisionObserver>();

            foreach (ICollisionObserver observer in observers)
            {
                observer.OnCollisionEnter(collision);
            }
        }
    }
}
