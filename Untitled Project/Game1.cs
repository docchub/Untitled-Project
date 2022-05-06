using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Untitled_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Gameplay
        private bool yourTurn;

        // Stack
        private Stack<Ability> stack;

        // Abilities
        private AbilityManager library;
        private Dictionary<string, Action<Entity>> dictionary;

        // Characters
        private Entity player;
        private Entity player2;
        private Entity player3;
        private Entity enemy;
        private Entity enemy2;
        private Entity enemy3;
        private List<Entity> players;
        private List<Entity> enemies;

        // Fonts
        private SpriteFont debug;
        private SpriteFont headline;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initialize Combat
            yourTurn = true;
            
            // Initialize stack
            stack = new Stack<Ability>();

            // Initialize Abilities
            library = new AbilityManager();
            dictionary = library.CreateLibrary();

            // Characters
            player = new Entity(100, 10, "John");
            player2 = new Entity(90, 15, "Todd");
            player3 = new Entity(75, 6, "Criela");
            enemy = new Entity(120, 20, "Loxus");
            enemy2 = new Entity(100, 15, "Ormod");
            enemy3 = new Entity(95, 10, "Garry");

            players = new List<Entity>();
            enemies = new List<Entity>();

            players.Add(player);
            players.Add(player2);
            players.Add(player3);

            enemies.Add(enemy);
            enemies.Add(enemy2);
            enemies.Add(enemy3);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            debug = Content.Load<SpriteFont>("debugfont");
            headline = Content.Load<SpriteFont>("headline");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            _spriteBatch.Begin();

            // ----------- Debugging Code -----------
            _spriteBatch.DrawString(
                headline, 
                String.Format("Energy: {0}", library.Resources), 
                new Vector2(290, 10), 
                Color.DarkCyan);
            _spriteBatch.DrawString(
                debug,
                String.Format("[Energy: {0}]", library.EnemyResources),
                new Vector2(670, 280),
                Color.DarkCyan);

            int count = 0;
            foreach (Entity e in players)
            {
                _spriteBatch.DrawString(
                    debug,
                    String.Format(
                        "Name: {0}\n" +
                        "Health: {1}\n" +
                        "Defense: {2}\n",
                        e.Name,
                        e.Health,
                        e.Defense),
                    new Vector2(40, 40 + (80*count)),
                    Color.Black);

                count++;
            }

            count = 0;
            foreach (Entity e in enemies)
            {
                _spriteBatch.DrawString(
                    debug,
                    String.Format(
                        "Name: {0}\n" +
                        "Health: {1}\n" +
                        "Defense: {2}\n",
                        e.Name,
                        e.Health,
                        e.Defense),
                    new Vector2(660, 40 + (80 * count)),
                    Color.Black);

                count++;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        // Methods
        private void PlayStack()
        {

        }
    }
}
