using bravens.Managers;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


namespace bravens.ObjectComponent.Components
{
    public class FinalBossBehavior : Component
    {
        private readonly Random _random = new();

        private readonly Transform transform;

        private readonly Sprite sprite;
        private FinalBossGun gun;

        private Animation animation;


        private TimeSpan timer;
        private int timerInSeconds;

        private float speed = 800.0f;
        private int xDirection = 1;
        private int newPositionCooldownInSeconds = 3;

        private int _timeLastCheckedInSeconds = 0;
        private int _timeToSwitchNextPosition;
        private Vector2 _currentDestination;
        private bool _isMovingToDestination = false;

        private int positionSwitchCount = 0;

        // Phase Management
        private List<BossPhase> _phases;
        private int _currentPhaseIndex = 0;
        private float _phaseDuration;
        private float _phaseTimer = 0f;
        private bool _phasesInitialized = false;
        private BossPhase _currentPhase;

        // Heavy Sequence
        private float _heavyProjectileDelayTimer = 0f;
        private float _heavyProjectileWaitDuration = 1f;
        private bool _shouldFireHeavyProjectile = false;

        // Player Target Sequence
        private float _projectileTimer = 0f;
        private const float PROJECTILE_FIRE_RATE = 0.25f;

        public FinalBossBehavior(GameObject parent) : base(parent, nameof(FinalBossBehavior))
        {
            transform = parent.GetComponent<Transform>();
            sprite = parent.GetComponent<Sprite>();
            gun = parent.GetComponent<FinalBossGun>();

            var spriteSheet = parent.Core.Content.Load<Texture2D>("FinalBoss");


            animation = new Animation(
            this,
            spriteSheet,
            frameWidth: 96,
            frameHeight: 128,
            frameCount: 2,
            frameTime: 0.2f
            );

            CenterBoss();

            timer = TimeSpan.Zero;
            timerInSeconds = (int)timer.TotalSeconds;

            _timeToSwitchNextPosition = timerInSeconds + newPositionCooldownInSeconds;
            _currentDestination = transform.Position;
        }

        public override void Update(GameTime deltaTime)
        {
            animation.Update(deltaTime);
            if (!_phasesInitialized) return;

            _phaseTimer += (float)deltaTime.ElapsedGameTime.TotalSeconds;

            if (_phaseTimer >= _phaseDuration && _currentPhaseIndex < _phases.Count - 1) 
            {
                _currentPhaseIndex++;
                _phaseTimer = 0f;
                OnPhaseChanged(_phases[_currentPhaseIndex]);
                Console.WriteLine($"Boss entered phase {_currentPhaseIndex + 1}");
            }
            

            if (_currentPhase == null) return;
            switch (_currentPhase.attackPattern) 
            {
                case "SpiralSequence":
                    ExecuteSpiralSequencePhase(deltaTime);
                    break;

                case "HeavySequence":
                    ExecuteHeavySequencePhase(deltaTime);
                    break;

                case "TargetPlayerSequence":
                    ExecuteTargetPlayerSequence(deltaTime);
                    break;

            }
        }

        public void InitializePhases(List<BossPhase> phases, float totalDuration) 
        {
            _phases = phases;
            _phaseDuration = totalDuration / phases.Count;
            _phasesInitialized = true;
            _currentPhaseIndex = 0;
            _phaseTimer = 0f;
            OnPhaseChanged(_phases[_currentPhaseIndex]);
            Console.WriteLine($"Boss phases initialized with {phases.Count} phases, {_phaseDuration}s each");
        }

        private void OnPhaseChanged(BossPhase newPhase) 
        {
            _currentPhase = newPhase;
        }

        private void ExecuteSpiralSequencePhase(GameTime deltaTime) 
        {

            timer += deltaTime.ElapsedGameTime;
            timerInSeconds = (int)timer.TotalSeconds;

            if (timerInSeconds >= _timeToSwitchNextPosition)
            {
                Console.WriteLine("switch positions");
                positionSwitchCount++;
                _currentDestination = DetermineNextPosition();
                _timeToSwitchNextPosition = timerInSeconds + newPositionCooldownInSeconds;
                _isMovingToDestination = true;

                if (positionSwitchCount % 2 != 0)
                {
                    gun.CreateAndFireBurstProjectiles();
                }

            }

            if (_isMovingToDestination)
            {
                Vector2 direction = _currentDestination - transform.Position;
                if (direction.Length() > 10f)
                {
                    direction.Normalize();
                    transform.Translate(direction * speed * (float)deltaTime.ElapsedGameTime.TotalSeconds);
                }
                else
                {
                    Console.WriteLine("Reached Position");
                    _isMovingToDestination = false;
                    if (positionSwitchCount % 2 != 0)
                    {
                        gun.CreateAndFireBurstProjectiles();
                    }
                    else
                    {
                        gun.StartSpiralAttack();
                    }

                }
            }

            _timeLastCheckedInSeconds = timerInSeconds;
        }

        private void ExecuteHeavySequencePhase(GameTime deltaTime)
        {
            timer += deltaTime.ElapsedGameTime;
            timerInSeconds = (int)timer.TotalSeconds;

            if (timerInSeconds >= _timeToSwitchNextPosition)
            {
                Console.WriteLine("switch positions");
                positionSwitchCount++;
                _currentDestination = DetermineNextPosition();
                _timeToSwitchNextPosition = timerInSeconds + newPositionCooldownInSeconds;
                _isMovingToDestination = true;
            }

            if (_isMovingToDestination)
            {
                Vector2 direction = _currentDestination - transform.Position;
                if (direction.Length() > 10f)
                {
                    direction.Normalize();
                    transform.Translate(direction * speed * (float)deltaTime.ElapsedGameTime.TotalSeconds);
                }
                else
                {
                    Console.WriteLine("Reached Position");
                    _isMovingToDestination = false;

                    gun.CreateAndFireCircularBurstProjectiles();
  
                    _heavyProjectileDelayTimer = _heavyProjectileWaitDuration;
                    _shouldFireHeavyProjectile = true;
                }
            }

            if (_shouldFireHeavyProjectile)
            {
                _heavyProjectileDelayTimer -= (float)deltaTime.ElapsedGameTime.TotalSeconds;
                if (_heavyProjectileDelayTimer <= 0f)
                {
                    gun.CreateAndFireHeavyProjectile();
                    _shouldFireHeavyProjectile = false;
                }
            }

            _timeLastCheckedInSeconds = timerInSeconds;
        }

        private void ExecuteTargetPlayerSequence(GameTime deltaTime)
        {
            timer += deltaTime.ElapsedGameTime;
            timerInSeconds = (int)timer.TotalSeconds;

            _projectileTimer += (float)deltaTime.ElapsedGameTime.TotalSeconds;

            if (_projectileTimer >= PROJECTILE_FIRE_RATE)
            {
                gun.CreateAndFirePlayerTargetedProjectiles();
                _projectileTimer = 0f;
            }

            if (timerInSeconds >= _timeToSwitchNextPosition)
            {
                Console.WriteLine("switch positions");
                positionSwitchCount++;
                _currentDestination = DetermineNextPosition();
                _timeToSwitchNextPosition = timerInSeconds + newPositionCooldownInSeconds;
                _isMovingToDestination = true;
            }

            if (_isMovingToDestination)
            {
                Vector2 direction = _currentDestination - transform.Position;
                if (direction.Length() > 10f)
                {
                    direction.Normalize();
                    transform.Translate(direction * speed * (float)deltaTime.ElapsedGameTime.TotalSeconds);
                }
                else
                {
                    Console.WriteLine("Reached Position");
                    _isMovingToDestination = false;
                }
            }

            _timeLastCheckedInSeconds = timerInSeconds;
        }

        private Vector2 DetermineNextPosition() 
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;

            var randMaxX = _random.Next(sprite.SpriteTexture.Width, graphics.PreferredBackBufferWidth - sprite.SpriteTexture.Width);
            var randMaxY = _random.Next(sprite.SpriteTexture.Height, (graphics.PreferredBackBufferHeight / 2) - sprite.SpriteTexture.Height);

            return new Vector2(randMaxX, randMaxY);
        }

        /// <summary>
        /// Center the boss on the screen.
        /// </summary>
        private void CenterBoss()
        {
            GraphicsDeviceManager graphics = GetGameObject().Core.GraphicsDeviceManager;

            transform.SetPositionX(graphics.PreferredBackBufferWidth / 2);
            transform.SetPositionY(graphics.PreferredBackBufferHeight / 2 - sprite.SpriteTexture.Height);
        }

        public override void Draw()
        {
            var spriteBatch = GetGameObject().Core.SpriteBatch;
            animation.Draw(spriteBatch, transform.Position);
        }
    }
}

