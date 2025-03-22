using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.Utilities
{
    public class WaitForSeconds
    {
        private float _duration;
        private float _elapsedTime;

        public WaitForSeconds(float duration)
        {
            _duration = duration;
            _elapsedTime = 0f;
        }

        public bool IsDone(float deltaTime)
        {
            _elapsedTime += deltaTime;
            return _elapsedTime >= _duration;
        }
    }
}
