using bravens.Managers;
using bravens.ObjectComponent.Enums;
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
    public class EnemyBProjectile : Component, ICollisionObserver
    {
        private GameObjectManager GameObjectManager { get; }

        private readonly Transform transform;
        private readonly Sprite sprite;

        private float speed = 30.0f;
        private int projectileDamage = 5;

        public EnemyBProjectile(GameObject parent) : base(parent, nameof(EnemyBProjectile))
        {
            GameObjectManager = parent.Core.GameObjectManager;
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
        }

        public override void Update(GameTime deltaTime)
        {
            GameObject projectileGameObject = GetGameObject();
            Transform transform = projectileGameObject.GetComponent<Transform>();
            transform.Translate(new Vector2(0.0f, speed * (float)deltaTime.ElapsedGameTime.TotalSeconds));

            if (!GameBounds.IsGameObjectVisible(projectileGameObject))
            {
                GameObjectManager.Destroy(projectileGameObject);
            }

            speed = speed + 5;
        }

        public void OnCollisionEnter(Collider collider)
        {
            if (collider.Tag == CollisionTag.Player)
            {
                collider.GetGameObject().GetComponent<Health>().DamageUnit(projectileDamage);
                GameObjectManager.Destroy(GetGameObject());
            }
        }
    }
}
