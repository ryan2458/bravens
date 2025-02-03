using bravens.ObjectComponent.Components;
using bravens.ObjectComponent.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.Factories
{
    public class GameObjectFactory
    {
        public static GameObject CreateGameObject(GameCore gameCore, GameObject parent = null, string name = null)
        {
            GameObject newGameObject = new GameObject(gameCore, parent, name);
            newGameObject.AddComponent<Transform>();
            newGameObject.AddComponent(() =>
            {
                Sprite newSprite = new Sprite(gameCore, newGameObject);
                return newSprite;
            });

            return newGameObject;
        }

    }
}
