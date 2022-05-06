using System;
using System.Collections.Generic;
using System.Text;

namespace Untitled_Project
{
    class Ability
    {
        // Fields
        private bool quick;
        private int cost;

        // Stack
        public delegate void stackEffects();
        private event stackEffects effects;

        // Properties
        public bool Quick { get { return quick; } }
        public int Cost { get { return cost; } }

        // Constructor
        public Ability(bool quick, int cost)
        {
            this.quick = quick;
            this.cost = cost;
        }
    }
}
