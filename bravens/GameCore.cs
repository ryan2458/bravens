using bravens.Managers;
using bravens.ObjectComponent.Components;
using bravens.ObjectComponent.Enums;
using bravens.ObjectComponent.Objects;
using bravens.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace bravens
{
    public class GameCore : Game
    {
        public GameObjectManager GameObjectManager { get; }

        public WaveManager WaveManager { get; }

        public LivesManager LivesManager { get; private set; }

        public GraphicsDeviceManager GraphicsDeviceManager { get; }

        public SpriteBatch SpriteBatch { get; protected set; }


        private Texture2D backgroundTexture;

        private SpriteFont gameFont;

        public bool IsGameOver { get; private set; } = false;

        public GameCore()
        {
            Content.Unload();
            GameObjectManager = new GameObjectManager(this);
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            WaveManager = new WaveManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var json = File.ReadAllText("Content/waves.json");
            var enemiesConfig = JsonSerializer.Deserialize<EnemyConfig>(json);
        }

        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            GraphicsDeviceManager.PreferredBackBufferWidth = 900;  // Width
            GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100; // subtract 100 to account for task bar.
            GraphicsDeviceManager.ApplyChanges();

            GameObjectManager.Initialize();
            LivesManager = new LivesManager(this);

            CreatePlayer();

            base.Initialize();
        }
        public void TriggerGameOver()
        {
            IsGameOver = true;
            GameObjectManager.ClearAllObjects();
        }
        protected override void LoadContent()
        {
            backgroundTexture = Content.Load<Texture2D>("Background");
            gameFont = Content.Load<SpriteFont>("gameFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // call update on all managers (currently just GameObjectManager)
            GameObjectManager.Update(gameTime);

            WaveManager.Update(gameTime);
            CollisionManager.CheckCollisions();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();



            SpriteBatch.Draw(
                backgroundTexture,
                new Rectangle(0, 0, GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight),
                Color.White
            );

            GameObjectManager.Draw();


            SpriteBatch.End();

            if (IsGameOver)
            {
                SpriteBatch.Begin();
                SpriteBatch.DrawString(gameFont, "GAME OVER", new Vector2(400, 300), Color.Red);
                SpriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public void CreatePlayer()
        {
            float x = GraphicsDeviceManager.PreferredBackBufferWidth / 2.0f;
            float y = GraphicsDeviceManager.PreferredBackBufferHeight - 100.0f;

            GameObject player = GameObjectManager.Create(new Vector2(x, y), null, "Player"); ;
            player.AddComponent<PlayerControls>();
            player.AddComponent<PlayerGun>();
            player.AddComponent(() => new Health(player, 1));

            player.GetComponent<Health>().Died += LivesManager.PlayerDiedEventHandler;
            player.GetComponent<Health>().LifeUp += LivesManager.PlayerHealedEventHandler;

            Console.WriteLine("Invincibility Started");
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                player.AddComponent<Collider>();
                player.GetComponent<Collider>().Tag = CollisionTag.Player;
                Console.WriteLine("Invincibility Ended");
            });
        }


        public void CreateEnemyTypeA(EnemyConfig enemyConfig)
        {
            Texture2D projectileSprite = Content.Load<Texture2D>(enemyConfig.projectile.type);

            GameObject enemyA = GameObjectManager.Create(null, null, "blank");
            enemyA.AddComponent<EnemyABehaviour>().speed = enemyConfig.speed;
            var gun = enemyA.AddComponent(() =>
            new EnemyAGun(
               enemyA,
                   (float)enemyConfig.timeBetweenProjectileInSeconds,
                   enemyConfig.projectile.speed,
                   enemyConfig.projectile.projectileDamage,
                   projectileSprite,
                   enemyConfig.projectile.movement
                )
            );
            // enemyA.AddComponent<EnemyAGun>().timeBetweenProjectileInSeconds = enemyConfig.timeBetweenProjectileInSeconds;
            enemyA.AddComponent<Collider>();
            enemyA.AddComponent(() => new Health(enemyA, enemyConfig.Health));
            enemyA.AddComponent(() => new EnemyDuration(this, enemyA, enemyConfig.duration));

            enemyA.GetComponent<Collider>().Tag = CollisionTag.Enemy;
        }

        public void CreateBoss()
        {                                                                                                                                                                                                
            GameObject boss = GameObjectManager.Create(null, null, "boss");
            boss.AddComponent<BossBehavior>();
            boss.AddComponent<BossGun>();
            boss.AddComponent<Collider>();
            boss.AddComponent(() => new Health(boss, 120));
            boss.AddComponent(() => new EnemyDuration(this, boss, 25f));

            boss.GetComponent<Collider>().Tag = CollisionTag.Enemy;
        }

        public void CreateEnemyTypeB(EnemyConfig enemyConfig)
        {
            Texture2D projectileSprite = Content.Load<Texture2D>(enemyConfig.projectile.type);

            GameObject enemyB = GameObjectManager.Create(null, null, "blank");
            enemyB.AddComponent<EnemyBBehaviour>().speed = enemyConfig.speed;
            // enemyB.AddComponent<EnemyBGun>().timeBetweenProjectileInSeconds = enemyConfig.timeBetweenProjectileInSeconds;
            var gun = enemyB.AddComponent(() =>
            new EnemyBGun(
               enemyB,
                   (float)enemyConfig.timeBetweenProjectileInSeconds,
                   enemyConfig.projectile.speed,
                   enemyConfig.projectile.projectileDamage,
                   projectileSprite,
                   enemyConfig.projectile.movement
                )
            );
            enemyB.AddComponent<Collider>();
            enemyB.AddComponent(() => new Health(enemyB, enemyConfig.Health));
            enemyB.AddComponent(() => new EnemyDuration(this, enemyB, enemyConfig.duration));

            enemyB.GetComponent<Collider>().Tag = CollisionTag.Enemy;
            enemyB.GetComponent<Collider>().Radius = 64;
        }

        public void CreateFinalBoss() 
        {
            GameObject finalBoss = GameObjectManager.Create("FinalBoss", null, "FinalBoss-large");
            finalBoss.AddComponent<FinalBossGun>();
            finalBoss.AddComponent<FinalBossBehavior>();
            finalBoss.AddComponent<Collider>();
            finalBoss.AddComponent(() => new Health(finalBoss, 200));
            finalBoss.AddComponent(() => new EnemyDuration(this, finalBoss, 120f));

            finalBoss.GetComponent<Collider>().Tag= CollisionTag.Enemy;
        }

        public void CreateLifeToken(float x, float y)
        {
            Vector2 pos = new Vector2(x,y);
            GameObject lifeToken = GameObjectManager.Create(pos, null, "plus");
            lifeToken.AddComponent<LifeTokenBehavior>();
            lifeToken.AddComponent<Collider>();
            lifeToken.AddComponent(() => new EnemyDuration(this, lifeToken, 10f));

            lifeToken.GetComponent<Collider>().Tag = CollisionTag.LifeToken;
        }
    }
}
