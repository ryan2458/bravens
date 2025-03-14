using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class MidBossBehavior : Component
    {
        private readonly Random _random = new();
        private readonly Transform transform;
        private readonly Sprite sprite;
        private readonly MidBossGun gun;

        private float speed = 200.0f;
        private int xDirection = 1;

        public MidBossBehavior(GameObject parent) : base(parent, nameof(MidBossBehavior))
        {
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
            gun = parent.GetComponent<MidBossGun>();

            CenterMidBoss();
        }

        public override void Update(GameTime deltaTime)
        {
            Vector2 movement = new Vector2(xDirection, 0);
            transform.Translate(movement * speed * (float)deltaTime.ElapsedGameTime.TotalSeconds);

            SwitchDirectionsIfNeeded();
            gun.Update(deltaTime); // Call the gun's update method to handle firing
        }

        private void SwitchDirectionsIfNeeded()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;

            if (transform.Position.X > graphics.PreferredBackBufferWidth - sprite.SpriteTexture.Width / 2)
            {
                xDirection = -1;
            }
            else if (transform.Position.X < sprite.SpriteTexture.Width / 2)
            {
                xDirection = 1;
            }
        }

        private void CenterMidBoss()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;
            transform.SetPositionX(graphics.PreferredBackBufferWidth / 2);
            transform.SetPositionY(100); // Set a specific Y position for the mid-boss
        }
    }
}
