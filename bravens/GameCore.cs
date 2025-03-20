using bravens.Managers;
using bravens.ObjectComponent.Components;
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

        private void CreatePlayer()
        {
            GameObject player = GameObjectManager.Create("Player", null);
            player.AddComponent<PlayerControls>();
            player.AddComponent<PlayerGun>();
            player.AddComponent<Collider>();

            player.GetComponent<Collider>().Tag = ObjectComponent.Enums.CollisionTag.Player;
        }

        public void CreateEnemyTypeA()
        {
            GameObject enemyA = GameObjectManager.Create(null, null, "square");
            enemyA.AddComponent<EnemyABehaviour>();
            enemyA.AddComponent<EnemyAGun>();
        }

        public void CreateBoss()
        {
            GameObject boss = GameObjectManager.Create(null, null, "boss");
            boss.AddComponent<BossBehavior>();
            boss.AddComponent<BossGun>();
        }

        public void CreateEnemyTypeB()
        {
            GameObject enemyB = GameObjectManager.Create(null, null, "square_2");
            enemyB.AddComponent<EnemyBBehaviour>();
            enemyB.AddComponent<EnemyBGun>();
        }
    }
}
