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
        public static GameObject CreateGameObject(GameObject parent = null)
        {
            GameObject newGameObject = new GameObject(parent);
            newGameObject.AddComponent<Transform>();
            newGameObject.AddComponent<Sprite>();

            return new GameObject(parent);
        }

    }
}
