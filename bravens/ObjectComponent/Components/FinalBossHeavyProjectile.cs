using bravens.Managers;
using bravens.ObjectComponent.Interfaces;
using bravens.ObjectComponent.Objects;
using bravens.Utilities;
using Microsoft.Xna.Framework;

namespace bravens.ObjectComponent.Components
{
    internal class FinalBossHeavyProjectile : Component, ICollisionObserver
    {
        private GameObjectManager GameObjectManager { get; }

        private readonly Transform transform;
        private readonly Sprite sprite;
        private readonly Vector2 direction;

        private readonly float speed = 200.0f;

        private int projectileDamage = 10;

        public FinalBossHeavyProjectile(GameObject parent, Vector2 direction) : base(parent, nameof(FinalBossHeavyProjectile))
        {
            GameObjectManager = parent.Core.GameObjectManager;
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
            this.direction = direction;
        }

        public override void Update(GameTime deltaTime)
        {
            GameObject projectileGameObject = GetGameObject();
            Transform transform = projectileGameObject.GetComponent<Transform>();
            transform.Translate(direction * speed * (float)deltaTime.ElapsedGameTime.TotalSeconds);

            if (!GameBounds.IsGameObjectVisible(projectileGameObject))
            {
                GameObjectManager.Destroy(projectileGameObject);
            }
        }

        private bool IsVisible()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;

            return !(transform.Position.Y > graphics.PreferredBackBufferHeight + sprite.SpriteTexture.Height) &&
                   !(transform.Position.Y < 0) &&
                   !(transform.Position.X > graphics.PreferredBackBufferWidth) &&
                   !(transform.Position.X < sprite.SpriteTexture.Width);
        }

        /// <inheritdoc />
        public void OnCollisionEnter(Collider collider)
        {
            if (collider.Tag == Enums.CollisionTag.Player)
            {
                collider.GetGameObject().GetComponent<Health>().DamageUnit(projectileDamage);
                GameObjectManager.Destroy(GetGameObject());
            }
        }
    }
}
