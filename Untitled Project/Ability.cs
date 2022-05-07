using System;
using System.Collections.Generic;
using System.Text;

namespace Untitled_Project
{
    class Ability
    {
        // Function
        private event AbilityManager.stackEffects ability;

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
        public AbilityManager.stackEffects Effect { get { return ability; } }

        // Constructor
        public Ability(string name, AbilityManager abm)
        {
            // Build the function of the ability
            ability += abm.GetAbility(name);

            // Get cost and misc data
            data = abm.GetAbilityInfo(name);
            this.name = data[0];
            quick = bool.Parse(data[1]);
            cost = int.Parse(data[2]);
        }
    }
}
