using bravens.ObjectComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace bravens.Managers
{
    public class WaveManager : BaseObject
    {
        public TimeSpan globalTimer;
        public int globalTimerInSeconds;

        private GameCore gameCore;
        private GameObjectManager gameObjectManager;

        private WaveConfig _waveConfig;
        private int _currentWaveIndex;
        private float _waveTimer;
        private float _spawnTimer;
        private Dictionary<string, int> _enemySpawnCounts;
        private bool _isBossSpawned;
        private bool _isFinalBossSpawned;

        private string _currentDifficulty;

        private double lastSpawnTime = 0;
        private double spawnCooldown = 500;
        private int _timeLastCheckedInSeconds = 0;
        public int GetCurrentWaveNumber() => _currentWaveIndex + 1;

        public WaveManager(GameCore core) : base(nameof(WaveManager))
        {
            gameCore = core;
            gameObjectManager = core.GameObjectManager;
            LoadWaveConfig();
            _currentWaveIndex = 0;
            _waveTimer = 0;
            _spawnTimer = 0;
            _enemySpawnCounts = new Dictionary<string, int>();
            _currentDifficulty = "normal";
        }

        private void LoadWaveConfig()
        {
            string jsonString = File.ReadAllText("Content/waves.json");
            _waveConfig = JsonSerializer.Deserialize<WaveConfig>(jsonString);
        }

        public override void Initialize() { }
        public override void Load() { }
        public override void Unload() { }

        public override void Update(GameTime gameTime)
        {
            globalTimer += gameTime.ElapsedGameTime;
            globalTimerInSeconds = (int)globalTimer.TotalSeconds;
            double currentTime = gameTime.TotalGameTime.TotalMilliseconds;

            if (_currentWaveIndex >= _waveConfig.waves.Count) return;

            var currentWave = _waveConfig.waves[_currentWaveIndex];
            _waveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var enemy in currentWave.enemies)
            {
                if (!_enemySpawnCounts.ContainsKey(enemy.type))
                {
                    _enemySpawnCounts[enemy.type] = 0;
                }

                if (_enemySpawnCounts[enemy.type] < enemy.count && _spawnTimer >= enemy.spawnDelay)
                {
                    SpawnEnemy(enemy);
                    _enemySpawnCounts[enemy.type]++;
                    _spawnTimer = 0;
                }
            }

            if (!_isBossSpawned && currentWave.boss != null && IsWaveEnemiesCleared(currentWave))
            {
                SpawnBoss(currentWave.boss);
                _isBossSpawned = true;
            }

            if (!_isFinalBossSpawned && currentWave.finalboss != null && IsWaveEnemiesCleared(currentWave))
            {
                SpawnFinalBoss(currentWave.finalboss);
                _isFinalBossSpawned = true;
            }

            if (_waveTimer >= currentWave.duration && IsWaveComplete(currentWave))
            {
                StartNextWave();
            }

            if (_timeLastCheckedInSeconds != globalTimerInSeconds)
            {
                Console.WriteLine($"⏱ Time: {globalTimerInSeconds}s | 🌊 Wave: {_currentWaveIndex + 1}");

                foreach (var kvp in _enemySpawnCounts)
                {
                    Console.WriteLine($"   - {kvp.Key}: {kvp.Value} spawned");
                }

                _timeLastCheckedInSeconds = globalTimerInSeconds;
            }

            if (_timeLastCheckedInSeconds != globalTimerInSeconds)
            {
                Console.WriteLine($"Current Spawn Timer: {globalTimerInSeconds}");
                _timeLastCheckedInSeconds = globalTimerInSeconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F1) && currentTime - lastSpawnTime >= spawnCooldown)
            {
                gameCore.CreateEnemyTypeA();
                lastSpawnTime = currentTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F2) && currentTime - lastSpawnTime >= spawnCooldown)
            {
                gameCore.CreateEnemyTypeB();
                lastSpawnTime = currentTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F3) && currentTime - lastSpawnTime >= spawnCooldown)
            {
                gameCore.CreateBoss();
                lastSpawnTime = currentTime;
            } 

            if (Keyboard.GetState().IsKeyDown(Keys.F4) && currentTime - lastSpawnTime >= spawnCooldown)
            {
                gameCore.CreateFinalBoss();
                lastSpawnTime = currentTime;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.F5) && currentTime - lastSpawnTime >= spawnCooldown)
            {
                gameCore.CreateLifeToken(500, 500);
                lastSpawnTime = currentTime;
            }

        }

        public override void Draw() { }

        private void SpawnEnemy(EnemyConfig enemy)
        {
            float healthMultiplier = _waveConfig.difficulties[_currentDifficulty].enemyHealthMultiplier;
            float speedMultiplier = _waveConfig.difficulties[_currentDifficulty].enemySpeedMultiplier;

            switch (enemy.type)
            {
                case "EnemyA":
                    gameCore.CreateEnemyTypeA();
                    break;
                case "EnemyB":
                    gameCore.CreateEnemyTypeB();
                    break;
                default:
                    Console.WriteLine("Unknown enemy type: " + enemy.type);
                    break;
            }
        }

        private void SpawnBoss(BossConfig boss)
        {
            float healthMultiplier = _waveConfig.difficulties[_currentDifficulty].enemyHealthMultiplier;

            switch (boss.type) 
            {
                case "Boss1":
                    Console.WriteLine("Spawning Boss!");
                    gameCore.CreateBoss();
                    break;
                case "FinalBoss":
                    Console.WriteLine("Spawning Final Boss!");
                    gameCore.CreateFinalBoss();
                    break;
            }

            // Extend boss logic if multiple boss types are supported
            
        }

        // Currently unused
        private void SpawnFinalBoss(BossConfig boss)
        {
            float healthMultiplier = _waveConfig.difficulties[_currentDifficulty].enemyHealthMultiplier;
            // Extend boss logic if multiple boss types are supported
            Console.WriteLine("Spawning Final Boss!");
            gameCore.CreateFinalBoss();
        }

        private bool IsWaveEnemiesCleared(Wave wave)
        {
            foreach (var enemy in wave.enemies)
            {
                if (_enemySpawnCounts[enemy.type] < enemy.count)
                    return false;
            }
            return true;
        }

        private bool IsWaveComplete(Wave wave)
        {
            if (!IsWaveEnemiesCleared(wave)) return false;
            if (wave.boss != null && !_isBossSpawned) return false;
            if (wave.finalboss != null && !_isFinalBossSpawned) return false;
            return true;
        }

        private void StartNextWave()
        {
            _currentWaveIndex++;
            _waveTimer = 0;
            _spawnTimer = 0;
            _enemySpawnCounts.Clear();
            _isBossSpawned = false;
            _isFinalBossSpawned = false;
            Console.WriteLine($" Starting Wave {_currentWaveIndex} complete. Starting wave {_currentWaveIndex + 1}.");
        }

        public void SetDifficulty(string difficulty)
        {
            if (_waveConfig.difficulties.ContainsKey(difficulty))
            {
                _currentDifficulty = difficulty;
            }
        }
    }

    public class WaveConfig
    {
        public List<Wave> waves { get; set; }
        public Dictionary<string, PowerUpConfig> powerUps { get; set; }
        public Dictionary<string, DifficultyConfig> difficulties { get; set; }
    }

    public class Wave
    {
        public int id { get; set; }
        public List<EnemyConfig> enemies { get; set; }
        public float duration { get; set; }
        public BossConfig boss { get; set; }
        public BossConfig finalboss { get; set; }
    }

    public class EnemyConfig
    {
        public string type { get; set; }
        public int count { get; set; }
        public float spawnDelay { get; set; }
        public int health { get; set; }
        public float dropRate { get; set; }
        public List<string> drops { get; set; }
    }

    public class BossConfig
    {
        public string type { get; set; }
        public int health { get; set; }
        public List<BossPhase> phases { get; set; }
    }

    public class BossPhase
    {
        public float healthThreshold { get; set; }
        public string attackPattern { get; set; }
    }

    public class PowerUpConfig
    {
        public int healAmount { get; set; }
        public int collectionsForExtraLife { get; set; }
        public int damage { get; set; }
        public int radius { get; set; }
    }

    public class DifficultyConfig
    {
        public float enemyHealthMultiplier { get; set; }
        public float enemySpeedMultiplier { get; set; }
        public float playerDamageMultiplier { get; set; }
    }
}
