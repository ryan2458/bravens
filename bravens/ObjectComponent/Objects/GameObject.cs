using bravens.ObjectComponent.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bravens.ObjectComponent.Objects
{
    /// <summary>
    /// Represents a single game object that exists in space.
    /// A game object is made up of zero or more components.
    /// </summary>
    public class GameObject : BaseObject
    {
        /// <summary>
        /// Internal counter for creating a unique game object name.
        /// This should not be used as an indicator for a current count of game objects.
        /// </summary>
        private static int gameObjectCountName = 0;

        /// <summary>
        /// Acts as a prefix for default game object naming.
        /// </summary>
        private static readonly string defaultNamePrefix = "GameObject";

        /// <summary>
        /// The components on this game object.
        /// </summary>
        private List<Component> Components { get; } = [];

        /// <summary>
        /// Gets the core monogame object.
        /// </summary>
        public GameCore Core { get; }

        /// <summary>
        /// Initializes a new <see cref="GameObject"/>
        /// </summary>
        /// <param name="gameCore">The core monogame object.</param>
        /// <param name="parent">Optional: a parent game object.</param>
        /// <param name="name">Optional: a custom name for this game object.</param>
        public GameObject(GameCore gameCore, GameObject parent = null, string name = null) : base(name ?? GenerateDefaultName())
        {
            Parent = parent;
            Core = gameCore;
        }

        public override void Initialize()
        {
            for (int i = 0; i < Components.Count; ++i)
            {
                Components[i].Initialize();
            }
        }

        /// <summary>
        /// Draws all components attached to this game object.
        /// </summary>
        public override void Draw()
        {
            for (int i = 0; i < Components.Count; ++i)
            {
                Components[i].Draw();
            }
        }

        /// <summary>
        /// Updates all components attached to this game object.
        /// </summary>
        /// <param name="deltaTime">Time passed since this method was last called.</param>
        public override void Update(GameTime deltaTime)
        {
            for (int i = 0; i < Components.Count; ++i)
            {
                Components[i].Update(deltaTime);
            }
        }

        public override void Unload()
        {
            for (int i = 0; i < Components.Count; ++i)
            {
                Components[i].Unload();
            }
        }

        /// <summary>
        /// Adds a component of type T to this game object.
        /// </summary>
        /// <typeparam name="T">The type of the component we're adding.</typeparam>
        public T AddComponent<T>() where T : Component
        {
            T newComponent = (T)Activator.CreateInstance(typeof(T), this);
            Components.Add(newComponent);
            return newComponent;
        }

        /// <summary>
        /// Adds a component using a given factory delegate
        /// </summary>
        /// <typeparam name="T">The type of component we're adding.</typeparam>
        /// <param name="factory"></param>
        public T AddComponent<T>(Func<T> factory) where T : Component
        {
            T newComponent = factory();
            Components.Add(newComponent);
            return newComponent;
        }

        /// <summary>
        /// Gets the first component on this game object that matches the type T.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        /// <returns>The first component found that matches type T.</returns>
        public T GetComponent<T>() where T : Component
        {
            return (T)Components.Where(c => c.GetType() == typeof(T)).FirstOrDefault();
        }

        /// <summary>
        /// Gets all components of type <typeparamref name="T"/> on this game object.
        /// </summary>
        /// <typeparam name="T">The specific component type</typeparam>
        /// <returns>A collection of components matching <typeparamref name="T"/></returns>
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

        /// <summary>
        /// Generates a default unique name for new game objects.
        /// </summary>
        private static string GenerateDefaultName()
        {
            return defaultNamePrefix + gameObjectCountName++; 
        }
    }
}
