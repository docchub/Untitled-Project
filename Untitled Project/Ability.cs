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
        private string name;

        // Stack
        public delegate void stackEffects(Entity e);
        private event stackEffects effects;

        // Properties
        public bool Quick { get { return quick; } }
        public int Cost { get { return cost; } }
        public string Name { get { return name; } }

        // Constructor
        public Ability(string name)
        {
        }
    }
}
