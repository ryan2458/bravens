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
                Console.WriteLine($"Current Lives: {Lives}");
                Lives -= 1;
            }
            else
            {
                GameOver();
            }
        }

        public void PlayerHealedEventHandler(object sender, GameObject healedObject)
        {
            if (Lives < 3)
            {
                Lives += 1;
                Console.WriteLine($"Current Lives: {Lives}");

                while(lifeIconObjects.Count >0)
                {
                    GameObject lifeIconObject = lifeIconObjects.LastOrDefault();
                    Core.GameObjectManager.Destroy(lifeIconObject);
                    lifeIconObjects.Remove(lifeIconObject);
                }

                float x = Core.GraphicsDeviceManager.PreferredBackBufferWidth - 35.0f;
                float y = Core.GraphicsDeviceManager.PreferredBackBufferHeight - 35.0f;

                for (int i = 0; i < Lives; ++i)
                {
                    GameObject lifeIcon = Core.GameObjectManager.Create(new Vector2(x - (i * 65), y), null, null, null);
                    lifeIconObjects.Add(lifeIcon);
                }
            }
        }

        public void GameOver()
        {
            Console.WriteLine("Game Over!");
            Core.TriggerGameOver();
        }
    }
}
