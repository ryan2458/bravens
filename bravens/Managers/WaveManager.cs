using bravens.ObjectComponent;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace bravens.Managers
{
    public class WaveManager : BaseObject
    {
        private GameCore gameCore;
        private GameObjectManager gameObjectManager;
        private TimeSpan globalTimer;

        private List<int> _enemyASpawnTimes = [5, 10, 20];
        private int _enemyASpawnIndex = 0;
        private int _previousTimeInSeconds = 0;

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
            
            if (_previousTimeInSeconds != (int)globalTimer.TotalSeconds) 
            {
                CheckEnemySpawn(_enemyASpawnTimes, ref _enemyASpawnIndex);
                Console.WriteLine((int)globalTimer.TotalSeconds);
                Console.WriteLine(($"Current Spawn Index: {_enemyASpawnIndex}"));
                _previousTimeInSeconds = (int)globalTimer.TotalSeconds;
            }

            
        }
        public override void Draw() { }

        private void CheckEnemySpawn(List<int> spawnTimes, ref int currentSpawnIndex) 
        {
            if (currentSpawnIndex >= spawnTimes.Count) return;

            if ((int)globalTimer.TotalSeconds == spawnTimes[currentSpawnIndex]) 
            {
                gameCore.CreateEnemyTypeA();

                currentSpawnIndex++;
            }          
        }
    }
}
