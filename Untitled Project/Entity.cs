using System;
using System.Collections.Generic;
using System.Text;

namespace Untitled_Project
{
    class Entity
    {
        // Fields
        private int maxHP;
        private int health;
        private int damage;
        private int defense;
        private string name;
        private List<Ability> abilities;

        // Properties
        public int MaxHealth { get { return maxHP; } }
        public int Health
        {
            get { return health; }

            // Don't reduce health below 0 or above the max
            set
            {
                // Current HP + value is between 0 and MAX HP
                if ((health + value) <= maxHP && (health + value) >= 0)
                {
                    health = value;
                }

                // Current HP + value is greater than MAX HP
                else if (health + value > maxHP)
                {
                    health = maxHP;
                }

                // Current HP + value is less than 0
                else if (health + value < 0)
                {
                    health = 0;
                }
            }
        }

        public int Damage { get { return damage; } }
        public int Defense { get { return defense; } set { defense = value; } }
        public string Name { get { return name; } }

        public List<Ability> Abilities { get { return abilities; } set { abilities = value; } }

        // Constructor
        public Entity(int maxHP, int damage, string name)
        {
            this.maxHP = maxHP;
            this.damage = damage;
            this.name = name;
            health = maxHP;
            defense = 0;
            abilities = new List<Ability>();
        }
    }
}
