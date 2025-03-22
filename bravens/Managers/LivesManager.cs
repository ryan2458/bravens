using bravens.ObjectComponent.Components;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.Managers
{
    public class LivesManager
    {
        private GameCore Core { get; }

        private List<GameObject> lifeIconObjects = new List<GameObject>(); 

        public int Lives { get; private set; } = 3;

        public LivesManager(GameCore core)
        {
            Core = core;

            float x = Core.GraphicsDeviceManager.PreferredBackBufferWidth - 35.0f;
            float y = Core.GraphicsDeviceManager.PreferredBackBufferHeight - 35.0f;

            for (int i = 0; i < Lives; ++i)
            {
                GameObject lifeIcon = Core.GameObjectManager.Create(new Vector2(x - (i * 65), y), null, null, null);
                lifeIconObjects.Add(lifeIcon);
            }
        }

        public void PlayerDiedEventHandler(object sender, GameObject dyingObject)
        {
            if (Lives > 0)
            {
                GameObject lifeIconObject = lifeIconObjects.LastOrDefault();
                Core.GameObjectManager.Destroy(lifeIconObject);
                lifeIconObjects.Remove(lifeIconObject);
                Core.CreatePlayer();
                Lives -= 1;
            }
            else
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            Console.WriteLine("Game Over!");
        }
    }
}
