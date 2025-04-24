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
    public class EnemyDuration : Component
    {
        private GameCore _core;
        private float _duration;
        private TimeSpan _elapsedTime;
        private int _elapsedTimeInSeconds;
        private bool _isActive;

        public EnemyDuration(GameCore core, GameObject parent, float duration) : base(parent, nameof(EnemyDuration))
        {
            _core = core;
            _duration = duration;
            _elapsedTime = TimeSpan.Zero;
            _isActive = true;
        }

        public override void Update(GameTime deltaTime)
        {
            if (!_isActive) return;

            _elapsedTime += deltaTime.ElapsedGameTime;
            // _elapsedTimeInSeconds = (int)_elapsedTime.TotalSeconds;

            if (_elapsedTimeInSeconds >= _duration) 
            {
                _isActive = false;
                _core.GameObjectManager.Destroy(GetGameObject());
                Console.WriteLine($"{GetGameObject().Name} has left.");
                Console.WriteLine($"[EnemyDuration] {GetGameObject().Name} lived for {_elapsedTime.TotalSeconds:F2}s, lifespan: {_duration}s");

            }
        }
    }
}
