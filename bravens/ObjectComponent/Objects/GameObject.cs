using bravens.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Objects
{
    public class GameObject : BaseObject
    {
        private static int gameObjectCounter = 0;

        private static readonly string defaultNamePrefix = "GameObject";

        private List<Component> Components { get; } = [];

        public GameObject(GameCore gameCore, GameObject parent = null) : base(GenerateDefaultName())
        {
            Parent = parent;
        }

        public override void Draw()
        {
            for (int i = 0; i < Components.Count; ++i)
            {
                Components[i].Draw();
            }
        }

        public override void Update(GameTime deltaTime)
        {
            for (int i = 0; i < Components.Count; ++i)
            {
                Components[i].Update(deltaTime);
            }
        }

        /// <summary>
        /// Adds a component of type T to this game object.
        /// </summary>
        /// <typeparam name="T">The type of the component we're adding.</typeparam>
        public void AddComponent<T>() where T : Component
        {
            T newComponent = (T)Activator.CreateInstance(typeof(T), this);
            Components.Add(newComponent);
        }

        /// <summary>
        /// Gets the first component on this game object that matches the type T.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        /// <returns>The first component found that matches type T.</returns>
        public Component GetComponent<T>()
        {
            return Components.Where(c => c.GetType() == typeof(T)).FirstOrDefault();
        }

        /// <summary>
        /// Gets all components of type T on this game object.
        /// </summary>
        /// <typeparam name="T">The specific component type</typeparam>
        /// <returns>A collection of components matching type T</returns>
        public IEnumerable<Component> GetComponents<T>()
        {
            return Components.Where(c => c.GetType() == typeof(T));
        }

        /// <summary>
        /// Gets a read-only collection of all components on this game object.
        /// </summary>
        /// <returns>A read-only collection of components</returns>
        public IEnumerable<Component> GetComponents()
        {
            return Components.AsReadOnly();
        }

        private static string GenerateDefaultName()
        {
            return defaultNamePrefix + gameObjectCounter++; 
        }
    }
}
