using System;
using System.Collections.Generic;
using System.Text;

namespace Untitled_Project
{
    abstract class GameObject
    {
        // Fields
        protected List<Ability> abilities;
        protected int maxHP;
        protected int health;
        protected int damage;

        // Properties
        public int Health
        {
            get { return health; }

            // Don't reduce health below 0 or above the max
            set
            {
                if (value <= maxHP || value >= 0)
                {
                    health = value;
                }
            }
        }

        public int Damage { get { return damage; } }

        // Constructor
        public GameObject(int maxHP, int damage, List<Ability> abilities)
        {
            this.maxHP = maxHP;
            this.health = maxHP;
            this.damage = damage;
            this.abilities = abilities;
        }
    }
}
