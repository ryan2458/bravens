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
        
        public LivesManager(GameCore core)
        {
            Core = core;
        }

        public int Lives { get; }

        public void Respawn()
        {
            Core.CreatePlayer();
        }

        public static void GameOver()
        {
            Console.WriteLine("Game Over!");
        }
    }
}
