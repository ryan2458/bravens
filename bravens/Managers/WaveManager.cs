using bravens.ObjectComponent;
using Microsoft.Xna.Framework;

namespace bravens.Managers
{
    public class WaveManager : BaseObject
    {
        private GameCore gameCore;
        private GameObjectManager gameObjectManager;

        public WaveManager(GameCore core) : base(nameof(WaveManager))
        {
            gameCore = core;
            gameObjectManager = core.GameObjectManager;
        }

        public override void Initialize() { }
        public override void Load() { }
        public override void Unload() { }
        public override void Update(GameTime deltaTime) { }
        public override void Draw() { }
    }
}
