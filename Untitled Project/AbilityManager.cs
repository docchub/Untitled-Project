using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Untitled_Project
{
    class AbilityManager
    {
        // File IO
        private const string filename = "abilities.txt";
        
        // Stack
        public delegate void stackEffects(Entity e);
        private event stackEffects effects;

        // Energy
        private int resources;
        private int enemyResources;
        private const int resourceMax = 10;

        // Saves each method to a key that is called when the ability is initialized
        private Dictionary<string, Action<Entity>> functionLibrary;

        // Holds the cost and whether or not a given ability is quick 
        private Dictionary<string, string[]> infoLibrary;

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

        /// <summary>
        /// Access the library anywhere
        /// </summary>
        public Dictionary<string, Action<Entity>> FuncLibrary { get { return functionLibrary; } }
        public Dictionary<string, string[]> InfoLibrary { get { return infoLibrary; } }

        public AbilityManager()
        {
            functionLibrary = new Dictionary<string, Action<Entity>>();
            infoLibrary = new Dictionary<string, string[]>();
        }

        #region Ability Dictionaries
        /// <summary>
        /// Create a dictionary for other classes to pull the data of abilities from
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Action<Entity>> BuildFunctionLibrary()
        {            
            functionLibrary.Add("Heal", Heal);
            functionLibrary.Add("Guard", Guard);
            functionLibrary.Add("Quick Guard", QuickGuard);
            functionLibrary.Add("Red Engine", RedEngine);

            return functionLibrary;
        }

        /// <summary>
        /// Get all ability names, costs, and activation time from a file
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string[]> BuildInfoLibrary()
        {
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(filename);
                string line = "";

                // Read each line of the file
                while ((line = sr.ReadLine()) != null)
                {
                    // Save the data as strings in an array
                    string[] s = new string[3];
                    s = line.Split('|');

                    // Create a bookmark
                    infoLibrary.Add(s[0], s);
                }
            }

            // Debugger messages
            catch (IOException ioe)
            {
                System.Diagnostics.Debug.WriteLine(
                    "File IO Error: " + ioe.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    "General Error: " + ex.Message);
            }

            // Close the streamreader
            if (sr != null)
            {
                sr.Close();
            }

            // Return the completed dictionary
            return infoLibrary;
        }

        /// <summary>
        /// Find and return an ability from the library
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public stackEffects GetAbility(string name)
        {
            if (functionLibrary.ContainsKey(name))
            {
                // Return the method searched for
                effects += functionLibrary[name].Invoke;
                return effects;
            }

            return default;
        }

        /// <summary>
        /// Find and return an ability's name, cost, and quick stat from the library
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string[] GetAbilityInfo(string name)
        {
            if (infoLibrary.ContainsKey(name))
            {
                return infoLibrary[name];
            }

            return default;
        }
        #endregion

        // ------------------ Abilities ------------------
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
