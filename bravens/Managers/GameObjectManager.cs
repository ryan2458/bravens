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
        public List<GameObject> GameObjects { get; } = [];

        public GameObjectManager() : base(nameof(GameObjectManager))
        {
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
