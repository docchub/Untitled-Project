using System;
using System.Collections.Generic;
using System.Text;

namespace Untitled_Project
{
    class Entity : GameObject
    {
        // Fields
        private string name;

        // Properties

        // Constructor
        public Entity(
            int maxHP, 
            int damage, 
            List<Ability> abilities, 
            string name)            
            : base(maxHP, damage, abilities)
        {
            this.name = name;
        }

        // Methods

        public void Attack(Entity target)
        {
            target.Health -= damage;
        }
    }
}
