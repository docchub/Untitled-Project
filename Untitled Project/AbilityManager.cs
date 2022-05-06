using System;
using System.Collections.Generic;
using System.Text;

namespace Untitled_Project
{
    class AbilityManager
    {
        // Stack
        public delegate void stackEffects(Entity e);
        private event stackEffects effects;

        // Energy
        private int resources;
        private int enemyResources;
        private const int resourceMax = 10;

        // Saves each method to a key that is called when the ability is initialized
        private Dictionary<string, Action<Entity>> library;

        /// <summary>
        /// Controls the game energy system for the player
        /// </summary>
        public int Resources
        {
            get { return resources; }

            set
            {
                // Cannot be reduced below 0 or above MAX
                if (resources + value < resourceMax && resources + value > 0)
                {
                    resources = value;
                }
                else if (resources + value > resourceMax)
                {
                    resources = resourceMax;
                }
            }
        }
        public int EnemyResources
        {
            get { return enemyResources; }

            set
            {
                // Cannot be reduced below 0 or above MAX
                if (enemyResources + value < resourceMax && enemyResources + value > 0)
                {
                    enemyResources = value;
                }
                else if (enemyResources + value > resourceMax)
                {
                    enemyResources = resourceMax;
                }
            }
        }

        public AbilityManager()
        {
            library = new Dictionary<string, Action<Entity>>();
        }

        /// <summary>
        /// Create a dictionary for other classes to pull the data of abilities from
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Action<Entity>> CreateLibrary()
        {
            library.Add("Heal", Heal);
            library.Add("Guard", Guard);
            library.Add("Quick Guard", QuickGuard);
            library.Add("Red Engine", RedEngine);

            return library;
        }

        /// <summary>
        /// Find and return an ability from the library
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public stackEffects GetAbility(string name)
        {
            if (library.ContainsKey(name))
            {
                effects += library[name].Invoke;
                return effects;
            }

            return default;
        }

        // Abilities
        #region Healing
        /// <summary>
        /// Healing Abilities
        /// </summary>
        /// <param name="self"></param>
        public void Heal(Entity self)
        {
            // Function
            self.Health += 20;
        }
        #endregion

        #region Guard
        /// <summary>
        /// Guard abilities
        /// </summary>
        /// <param name="self"></param>
        public void Guard(Entity self)
        {
            // Function
            self.Defense += 10;
        }
        public void QuickGuard(Entity self)
        {
            // Function
            self.Defense += 5;
        }
        #endregion

        /// <summary>
        /// Spend HP to gain resources
        /// </summary>
        public void RedEngine(Entity self)
        {
            // Function
            self.Health -= (self.MaxHealth * 80/100);
            resources += 5;
        }
    }
}
