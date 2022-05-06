using System;
using System.Collections.Generic;
using System.Text;

namespace Untitled_Project
{
    class Ability : AbilityManager
    {
        // Fields
        private event stackEffects ability;
        private bool quick;
        private int cost;
        private string name;

        /// <summary>
        /// Basic properties of abilities
        /// </summary>
        public bool Quick { get { return quick; } }
        public int Cost { get { return cost; } }
        public string Name { get { return name; } }

        // Constructor
        public Ability()
        {
            AbilityManager a = new AbilityManager();

            ability += a.GetAbility(name);
        }
    }
}
