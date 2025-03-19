using bravens.Managers;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class Projectile : Component
    {
        private GameObjectManager GameObjectManager { get; }

        private float speed = 500.0f;

        public Projectile(GameObject parent) : base(parent, nameof(Projectile))
        {
            GameObjectManager = parent.Core.GameObjectManager;
        }

        public override void Update(GameTime deltaTime)
        {
            GameObject projectileGameObject = GetGameObject();

            Transform transform = projectileGameObject.GetComponent<Transform>();

            transform.Translate(new Vector2(0.0f, -speed * (float)deltaTime.ElapsedGameTime.TotalSeconds));


            if (!IsVisible())
            {
                GameObjectManager.Destroy(GetGameObject());
            }
        }

        private bool IsVisible()
        {
            // TODO: Check if we're in screen bounds.
            return true;
        }
    }
}
