using bravens.ObjectComponent;
using bravens.ObjectComponent.Components;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public GameObject FindGameObjectByName(string objectName)
        {
            return GameObjects.Where(go => go.Name.Equals(objectName)).FirstOrDefault();
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

        public GameObject Create(string objectName = null, GameObject parent = null, string texturePath = null)
        {
            if (GameObjects.Select(go => go.Name).Any(n => n.Equals(objectName)))
            {
                throw new Exception("GameObject names must be unique.");
            }

            if (texturePath == null) texturePath = "ball";

            GameObject newGameObject = new GameObject(gameCore, parent, objectName);
            newGameObject.AddComponent<Transform>();
            newGameObject.AddComponent<Sprite>(() => new Sprite(newGameObject, texturePath));

            GameObjects.Add(newGameObject);
            return newGameObject;
        }

        public void Destroy(GameObject gameObject)
        {
            GameObjects.Remove(gameObject);
        }
    }
}
