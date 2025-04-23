using bravens.Managers;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bravens.ObjectComponent.Components
{
    public class Health : Component
    {
        private readonly GameObjectManager gameObjectManager;

        private readonly GameCore core;

        public int MaxHealth { get; }

        public int CurrentHealth { get; private set; }

        public event EventHandler<GameObject> Died = delegate { };
        public event EventHandler<GameObject> LifeUp = delegate { };

        public Health(GameObject parent, int maxHealth) : base(parent, nameof(Health))
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;

            gameObjectManager = parent.Core.GameObjectManager;
            core = parent.Core;
        }

        public void DamageUnit(int damageAmount)
        {
            CurrentHealth -= damageAmount;
            Console.WriteLine($"Damaged unit {damageAmount}");

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public void HealUnit(int healAmount)
        {
            CurrentHealth += healAmount;

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }

            LifeUp(this, GetGameObject());
        }

        public void Die()
        {
            Random rand = new Random();
            int lifeChance = rand.Next(0, 19);
            System.Console.WriteLine(GetGameObject().GetComponent<PlayerControls>());
            if(lifeChance == 0 && GetGameObject().GetComponent<PlayerControls>() == null)
            {
                core.CreateLifeToken(GetGameObject().GetComponent<Transform>().Position.X, GetGameObject().GetComponent<Transform>().Position.Y);
            }
            Died(this, GetGameObject());
            Died = null; // clear subscriptions before we delete this game object.
            gameObjectManager.Destroy(GetGameObject());
        }
    }
}
