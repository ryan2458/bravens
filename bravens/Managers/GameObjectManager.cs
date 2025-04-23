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

        public GameObject Create(Vector2 position, GameObject parent = null, string objectName = null, string texturePath = null)
        {
            GameObject newGameObject = Create(objectName, parent, texturePath);
            newGameObject.GetComponent<Transform>().SetPositionXY(position.X, position.Y);
            return newGameObject;
        }

        public GameObject Create(string objectName = null, GameObject parent = null, string texturePath = null)
        {
            if (texturePath == null) texturePath = "Player";

            GameObject newGameObject = new GameObject(gameCore, parent, objectName);
            newGameObject.AddComponent<Transform>();
            newGameObject.AddComponent<Sprite>(() => new Sprite(newGameObject, texturePath));

            GameObjects.Add(newGameObject);
            return newGameObject;
        }

        public void Destroy(GameObject gameObject)
        {
            if (gameObject != null)
            {
                gameObject.Unload();
                GameObjects.Remove(gameObject);
            }
        }
        public void ClearAllObjects()
        {
            // Create a copy of the list to avoid modification during iteration
            foreach (var gameObject in GameObjects.ToList())
            {
                Destroy(gameObject);
            }
        }
    }
}
