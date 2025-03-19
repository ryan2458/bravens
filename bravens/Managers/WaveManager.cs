using bravens.ObjectComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace bravens.Managers
{
    public class WaveManager : BaseObject
    {
        private GameCore gameCore;
        private GameObjectManager gameObjectManager;
        private TimeSpan globalTimer;
        private int globalTimerInSeconds;

        // Spawn times need to be in ascending order and there cannot be any repeats. If you have [5,5], it won't ever read the second 5.

        private List<int> _enemyASpawnTimes = [99];
        private int _enemyASpawnIndex = 0;

        private List<int> _enemyBSpawnTimes = [];
        private int _enemyBSpawnIndex = 0;


        private List<int> _bossSpawnTimes = [];
        private int _bossSpawnIndex = 0;


        private int _timeLastCheckedInSeconds = 0;
        
        private double lastSpawnTime = 0; // for using 1, 2, 3 to debug enemies
        private double spawnCooldown = 500; // Delay between shots in milliseconds

        public WaveManager(GameCore core) : base(nameof(WaveManager))
        {
            gameCore = core;
            gameObjectManager = core.GameObjectManager;
        }

        public override void Initialize() { }
        public override void Load() { }
        public override void Unload() { }
        public override void Update(GameTime deltaTime) 
        {
            globalTimer += deltaTime.ElapsedGameTime;
            globalTimerInSeconds = (int)globalTimer.TotalSeconds;

            double currentTime = deltaTime.TotalGameTime.TotalMilliseconds;

            // This is to prevent spawning multiple enemies of the same type at once
            if (_timeLastCheckedInSeconds != globalTimerInSeconds) 
            {
                Console.WriteLine($"Current Spawn Timer: {globalTimerInSeconds}");

                CheckEnemySpawn(_enemyASpawnTimes, ref _enemyASpawnIndex, "EnemyA");
                CheckEnemySpawn(_enemyBSpawnTimes, ref _enemyBSpawnIndex, "EnemyB");
                CheckEnemySpawn(_bossSpawnTimes, ref _bossSpawnIndex, "Boss");

                _timeLastCheckedInSeconds = globalTimerInSeconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D1) && currentTime - lastSpawnTime >= spawnCooldown)
            {
                gameCore.CreateEnemyTypeA();
                lastSpawnTime = currentTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2) && currentTime - lastSpawnTime >= spawnCooldown)
            {
                gameCore.CreateEnemyTypeB();
                lastSpawnTime = currentTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3) && currentTime - lastSpawnTime >= spawnCooldown)
            {
                gameCore.CreateBoss();
                lastSpawnTime = currentTime;
            }


        }
        public override void Draw() { }

        private void CheckEnemySpawn(List<int> spawnTimes, ref int currentSpawnIndex, string enemyType) 
        {
            if (currentSpawnIndex >= spawnTimes.Count) return;

            if (globalTimerInSeconds == spawnTimes[currentSpawnIndex]) 
            {
                switch (enemyType) 
                {
                    case "EnemyA":
                        gameCore.CreateEnemyTypeA();
                        break;
                    case "EnemyB":
                        gameCore.CreateEnemyTypeB();
                        break;
                    case "Boss":
                        gameCore.CreateBoss();
                        break;
                    default:
                        Console.WriteLine("Could not find enemy type. Spawning EnemyA...");
                        gameCore.CreateEnemyTypeA();
                        break;
                }

                

                currentSpawnIndex++;
            }          
        }
    }
}
