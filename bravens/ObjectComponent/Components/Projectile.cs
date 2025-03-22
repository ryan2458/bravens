using bravens.Managers;
using bravens.ObjectComponent.Interfaces;
using bravens.ObjectComponent.Objects;
using bravens.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class Projectile : Component, ICollisionObserver
    {
        private GameObjectManager GameObjectManager { get; }

        private float speed = 500.0f;
        private int projectileDamage = 5;

        public Projectile(GameObject parent) : base(parent, nameof(Projectile))
        {
            GameObjectManager = parent.Core.GameObjectManager;
        }

        public override void Update(GameTime deltaTime)
        {
            GameObject projectileGameObject = GetGameObject();
            Transform transform = projectileGameObject.GetComponent<Transform>();
            transform.Translate(new Vector2(0.0f, -speed * (float)deltaTime.ElapsedGameTime.TotalSeconds));

            if (!GameBounds.IsGameObjectVisible(projectileGameObject))
            {
                GameObjectManager.Destroy(projectileGameObject);
            }
        }

        public void OnCollisionEnter(Collider collider)
        {
            if (collider.Tag == Enums.CollisionTag.Enemy)
            {
                collider.GetGameObject().GetComponent<Health>().DamageUnit(projectileDamage);
            }
        }
    }
}
