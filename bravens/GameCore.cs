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
        private GameObjectManager gameObjectManager;

        public GraphicsDeviceManager GraphicsDeviceManager { get; }

        public SpriteBatch SpriteBatch { get; protected set; }

        public GameCore()
        {
            gameObjectManager = new GameObjectManager(this);
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            CreatePlayer();
            CreateEnemyTypeA();

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
            gameObjectManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);
            gameObjectManager.Draw();

            base.Draw(gameTime);
        }

        private void CreatePlayer()
        {
            GameObject player = gameObjectManager.Create("Player");
            player.AddComponent<PlayerControls>();
        }

        private void CreateEnemyTypeA() 
        {
            GameObject enemyA = gameObjectManager.Create("EnemyA");
            enemyA.AddComponent<EnemyABehaviour>();
        }
    }
}
