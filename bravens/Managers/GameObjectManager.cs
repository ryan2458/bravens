using bravens.Factories;
using bravens.ObjectComponent;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.Managers
{
    public class GameObjectManager : BaseObject
    {
        private GameCore gameCore;

        public List<GameObject> GameObjects { get; } = [];

        public GameObjectManager(GameCore core) : base(nameof(GameObjectManager))
        {
            gameCore = core;
        }

        public void CreateGameObject()
        {
            GameObject newGameObject = GameObjectFactory.CreateGameObject();
            GameObjects.Add(newGameObject);
        }

        public override void Draw()
        {
            for (int i = 0; i < GameObjects.Count; ++i)
            {
                GameObjects[i].Draw();
            }
        }

        public override void Initialize()
        {
            for (int i = 0; i < GameObjects.Count; ++i)
            {
                GameObjects[i].Initialize();
            }
        }

        public override void Load()
        {
            for (int i = 0; i < GameObjects.Count; ++i)
            {
                GameObjects[i].Load();
            }
        }

        public override void Unload()
        {
            for (int i = 0; i < GameObjects.Count; ++i)
            {
                GameObjects[i].Unload();
            }
        }

        public override void Update(GameTime deltaTime)
        {
            for (int i = 0; i < GameObjects.Count; ++i)
            {
                GameObjects[i].Update(deltaTime);
            }
        }
    }
}
