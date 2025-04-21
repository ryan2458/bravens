using bravens.Managers;
using bravens.ObjectComponent.Objects;
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

        public int MaxHealth { get; }

        public int CurrentHealth { get; private set; }

        public event EventHandler<GameObject> Died = delegate { };
        public event EventHandler<GameObject> LifeUp = delegate { };

        public Health(GameObject parent, int maxHealth) : base(parent, nameof(Health))
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;

            gameObjectManager = parent.Core.GameObjectManager;
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
            Died(this, GetGameObject());
            Died = null; // clear subscriptions before we delete this game object.
            gameObjectManager.Destroy(GetGameObject());
        }
    }
}
