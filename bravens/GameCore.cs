using bravens.Managers;
using bravens.ObjectComponent.Components;
using bravens.ObjectComponent.Enums;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace bravens
{
    public class GameCore : Game
    {
        public GameObjectManager GameObjectManager { get; }

        public WaveManager WaveManager { get; }

        public LivesManager LivesManager { get; private set; }

        public GraphicsDeviceManager GraphicsDeviceManager { get; }

        public SpriteBatch SpriteBatch { get; protected set; }

        public GameCore()
        {
            GameObjectManager = new GameObjectManager(this);
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            WaveManager = new WaveManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

        protected override void LoadContent()
        {
            
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
            GameObjectManager.Draw();

            base.Draw(gameTime);
        }

        public void CreatePlayer()
        {
            float x = GraphicsDeviceManager.PreferredBackBufferWidth / 2.0f;
            float y = GraphicsDeviceManager.PreferredBackBufferHeight - 100.0f;

            GameObject player = GameObjectManager.Create(new Vector2(x, y), null, "Player"); ;
            player.AddComponent<PlayerControls>();
            player.AddComponent<PlayerGun>();
            player.AddComponent<Collider>();
            player.AddComponent(() => new Health(player, 10));

            player.GetComponent<Collider>().Tag = CollisionTag.Player;
            player.GetComponent<Health>().Died += LivesManager.PlayerDiedEventHandler;
        }

        public void CreateEnemyTypeA()
        {
            GameObject enemyA = GameObjectManager.Create(null, null, "square");
            enemyA.AddComponent<EnemyABehaviour>();
            enemyA.AddComponent<EnemyAGun>();
            enemyA.AddComponent<Collider>();
            enemyA.AddComponent(() => new Health(enemyA, 10));

            enemyA.GetComponent<Collider>().Tag = CollisionTag.Enemy;
        }

        public void CreateBoss()
        {
            GameObject boss = GameObjectManager.Create(null, null, "boss");
            boss.AddComponent<BossBehavior>();
            boss.AddComponent<BossGun>();
            boss.AddComponent<Collider>();
            boss.AddComponent(() => new Health(boss, 120));

            boss.GetComponent<Collider>().Tag = CollisionTag.Enemy;
        }

        public void CreateEnemyTypeB()
        {
            GameObject enemyB = GameObjectManager.Create(null, null, "square_2");
            enemyB.AddComponent<EnemyBBehaviour>();
            enemyB.AddComponent<EnemyBGun>();
            enemyB.AddComponent<Collider>();
            enemyB.AddComponent(() => new Health(enemyB, 20));

            enemyB.GetComponent<Collider>().Tag = CollisionTag.Enemy;
        }
    }
}
