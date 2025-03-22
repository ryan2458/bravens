using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.Utilities
{
    public class CoroutineRunner
    {
        private List<IEnumerator> _coroutines = new List<IEnumerator>();

        /// <summary>
        /// Adds new coroutine to list of coroutines.
        /// </summary>
        /// <param name="coroutine"></param>
        public void StartCoroutine(IEnumerator coroutine)
        {
            _coroutines.Add(coroutine);
        }

        /// <summary>
        /// Iterates through each coroutine. 
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            foreach (IEnumerator coroutine in _coroutines) 
            {
                // If our yield instruction is WaitForSeconds, we check if it is done.
                if (coroutine.Current is WaitForSeconds waitForSeconds) 
                {
                    if (!waitForSeconds.IsDone(deltaTime)) 
                    {
                        continue;
                    }
                }

                if (!coroutine.MoveNext()) 
                {
                    _coroutines.Remove(coroutine); // Delete coroutine if finished.
                }
            }
        }
    }
}
