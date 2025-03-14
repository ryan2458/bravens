using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class FinalBossBehavior : Component
    {
        private readonly Transform transform;
        private readonly Sprite sprite;
        private readonly FinalBossGun gun;

        private float speed = 220.0f;
        public int CurrentStage { get; private set; } = 1; // 1 for stage 1, 2 for stage 2
        private double stageDuration = 10.0; // Duration for each stage
        private double elapsedTime = 0.0;

        public FinalBossBehavior(GameObject parent) : base(parent, nameof(FinalBossBehavior))
        {
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
            gun = parent.GetComponent<FinalBossGun>();

            CenterFinalBoss();
        }

        public override void Update(GameTime deltaTime)
        {
            elapsedTime += deltaTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime > stageDuration)
            {
                SwitchStage();
            }

            gun.Update(deltaTime); // Call the gun's update method to handle firing
        }

        private void SwitchStage()
        {
            CurrentStage = CurrentStage == 1 ? 2 : 1; // Toggle between stages
            elapsedTime = 0.0; // Reset elapsed time for the new stage

            // Implement any additional logic for switching stages
            if (CurrentStage == 2)
            {
                speed = 250.0f; // Increase speed in stage 2
            }
            else
            {
                speed = 200.0f; // Reset speed in stage 1
            }
        }

        private void CenterFinalBoss()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;
            transform.SetPositionX(graphics.PreferredBackBufferWidth / 2);
            transform.SetPositionY(50); // Set a specific Y position for the final boss
        }
    }
}