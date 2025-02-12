﻿using bravens.ObjectComponent;
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
        private int globalTimerInSeconds;

        private List<int> _enemyASpawnTimes = [5,7,10,15,18];
        private int _enemyASpawnIndex = 0;

        private List<int> _enemyBSpawnTimes = [13, 19];
        private int _enemyBSpawnIndex = 0;


        private List<int> _bossSpawnTimes = [25];
        private int _bossSpawnIndex = 0;


        private int _timeLastCheckedInSeconds = 0;

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
            
            // This is to prevent spawning multiple enemies of the same type at once
            if (_timeLastCheckedInSeconds != globalTimerInSeconds) 
            {
                Console.WriteLine($"Current Spawn Timer: {globalTimerInSeconds}");

                CheckEnemySpawn(_enemyASpawnTimes, ref _enemyASpawnIndex, "EnemyA");
                CheckEnemySpawn(_enemyBSpawnTimes, ref _enemyBSpawnIndex, "EnemyB");
                CheckEnemySpawn(_bossSpawnTimes, ref _bossSpawnIndex, "Boss");


                _timeLastCheckedInSeconds = globalTimerInSeconds;
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
