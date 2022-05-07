using System;
using System.Collections.Generic;
using System.Text;

namespace Untitled_Project
{
    class Ability : AbilityManager
    {
        // Function
        private event stackEffects ability;

        // Data
        private string[] data;
        private string name;
        private bool quick;
        private int cost;

        /// <summary>
        /// Basic properties of abilities
        /// </summary>
        public string Name { get { return name; } }
        public bool Quick { get { return quick; } }
        public int Cost { get { return cost; } }
        public stackEffects Effect { get { return ability; } }

        // Constructor
        public Ability(string name)
        {
            // Build the function of the ability
            ability += GetAbility(name);

            // Get cost and misc data
            data = GetAbilityInfo(name);
            this.name = data[0];
            quick = bool.Parse(data[1]);
            cost = int.Parse(data[2]);
        }
    }
}
