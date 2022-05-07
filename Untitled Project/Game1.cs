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
        private List<Button> buttons;
        private List<Button> abilityButtons;
        private Entity selected;
        private Entity prevSelected;
        private Entity target;
        private KeyboardState kState;
        private KeyboardState prevKState;
        private MouseState mState;
        private MouseState prevMState;

        // Stack
        private Stack<Ability> stack;

        // Abilities
        private AbilityManager abManager;
        private Dictionary<string, Action<Entity>> library;
        private Dictionary<string, string[]> infoLibrary;
        private const int resourceMax = 10;

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
        private SpriteFont abilityText;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initialize stack
            stack = new Stack<Ability>();

            // Initialize Abilities
            abManager = new AbilityManager();
            library = abManager.BuildFunctionLibrary();
            infoLibrary = abManager.BuildInfoLibrary();

            // Initialize Combat
            yourTurn = true;
            abManager.Resources = resourceMax;
            abManager.EnemyResources = resourceMax;

            // Abilities
            Ability heal = new Ability("Heal", abManager);
            Ability guard = new Ability("Guard", abManager);
            Ability quickGuard = new Ability("Quick Guard", abManager);
            Ability redEngine = new Ability("Red Engine", abManager);

            // Characters
            player = new Entity(100, 10, "John");
            player2 = new Entity(90, 15, "Todd");
            player3 = new Entity(75, 6, "Criela");
            enemy = new Entity(120, 20, "Loxus");
            enemy2 = new Entity(100, 15, "Ormod");
            enemy3 = new Entity(95, 10, "Garry");

            players = new List<Entity>();
            enemies = new List<Entity>();

            // Add characters to requisite lists
            players.Add(player);
            players.Add(player2);
            players.Add(player3);
            enemies.Add(enemy);
            enemies.Add(enemy2);
            enemies.Add(enemy3);

            // Assign abilities
            player.Abilities.Add(heal);
            player.Abilities.Add(guard);
            player2.Abilities.Add(guard);
            player2.Abilities.Add(quickGuard);
            player3.Abilities.Add(heal);
            player3.Abilities.Add(redEngine);
            player3.Abilities.Add(guard);

            // Select Character
            buttons = new List<Button>();
            buttons.Add(new Button(new Rectangle(35, 35, 110, 65), player));
            buttons.Add(new Button(new Rectangle(35, 115, 110, 65), player2));
            buttons.Add(new Button(new Rectangle(35, 195, 110, 65), player3));

            // Select Abilities
            abilityButtons = new List<Button>();
            abilityButtons.Add(new Button(new Rectangle(70, 290, 200, 40)));
            abilityButtons.Add(new Button(new Rectangle(70, 340, 200, 40)));
            abilityButtons.Add(new Button(new Rectangle(70, 390, 200, 40)));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            debug = Content.Load<SpriteFont>("debugfont");
            headline = Content.Load<SpriteFont>("headline");
            abilityText = Content.Load<SpriteFont>("abilitytext");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            kState = Keyboard.GetState();
            mState = Mouse.GetState();

            // Pop the stack and resolve an ability
            if (SingleKeyPress(Keys.Enter, kState))
            {
                if (stack.Count > 0)
                {
                    PlayStack();
                }
                else if (stack.Count == 0)
                {
                    yourTurn = !yourTurn;
                }
            }

            // Highlights buttons when hovered over
            foreach (Button b in buttons)
            {
                HighlightButton(b);
            }

            // Ability buttons
            if (selected != null)
            {
                foreach (Button b in abilityButtons)
                {
                    HighlightButton(b);
                }
            }

            prevSelected = selected;
            prevKState = kState;
            prevMState = mState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            // ------------ ShapeBatch --------------
            ShapeBatch.Begin(GraphicsDevice);

            // Draw the buttons
            foreach (Button b in buttons)
            {
                ShapeBatch.Box(b.Rect, b.Color);
            }

            // Show player abilities
            if (selected != null)
            {
                foreach (Button b in abilityButtons)
                {
                    ShapeBatch.Box(b.Rect, b.Color);
                    ShapeBatch.Circle(
                        new Vector2(
                            b.Rect.X - 30, 
                            b.Rect.Y + 20), 
                        20, 
                        Color.DarkSalmon);
                }
            }

            ShapeBatch.End();

            // ----------- Debugging Code -----------
            _spriteBatch.Begin();
            _spriteBatch.DrawString(
                headline, 
                String.Format("Energy: {0}", abManager.Resources), 
                new Vector2(290, 10), 
                Color.DarkCyan);
            _spriteBatch.DrawString(
                debug,
                String.Format("[Energy: {0}]", abManager.EnemyResources),
                new Vector2(715, 5),
                Color.DarkCyan);            

            // Writes player data to the screen
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

            // Writes enemy data to the screen
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

            // Print message if the stack is empty
            if (stack.Count == 0)
            {
                _spriteBatch.DrawString(
                    debug,
                    String.Format("Stack is Empty"),
                    new Vector2(340, 350),
                    Color.DimGray);
            }

            // Print message that shows if it is your turn
            if (yourTurn)
            {
                _spriteBatch.DrawString(
                    debug,
                    String.Format("(Your Turn)"),
                    new Vector2(355, 370),
                    Color.LightCoral);
            }

            // Ability button text
            if (selected != null)
            {
                int i = 0;
                
                foreach (Ability a in selected.Abilities)
                {
                    _spriteBatch.DrawString(
                        abilityText,
                        String.Format(a.Name),
                        new Vector2(abilityButtons[i].Rect.X + 5, abilityButtons[i].Rect.Y + 5),
                        Color.Black);

                    _spriteBatch.DrawString(
                        abilityText, 
                        String.Format("{0}", a.Cost), 
                        new Vector2(abilityButtons[i].Rect.X - 38, abilityButtons[i].Rect.Y + 5), 
                        Color.Black);

                    if (i < 2)
                    {
                        i++;
                    }
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        // Methods
        /// <summary>
        /// Return true only when a key is first pressed
        /// </summary>
        /// <param name="key"></param>
        /// <param name="currentKbState"></param>
        /// <returns></returns>
        private bool SingleKeyPress(Keys key, KeyboardState currentKbState)
        {
            if (true)
            {
                if (currentKbState.IsKeyDown(key) && prevKState.IsKeyUp(key))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Resolve the ability on top of the stack
        /// </summary>
        private void PlayStack()
        {
            Ability current = null;

            if (stack.Count > 0)
            {
                current = stack.Pop();
                current.Effect.Invoke(target);
            }
        }

        /// <summary>
        /// Highlights and selects a button when you hover or click it
        /// </summary>
        /// <param name="b"></param>
        private void HighlightButton(Button b)
        {
            // Don't change color if the button is currently selected
            if (b.Rect.Contains(mState.Position) && selected != b.Selected)
            {
                b.Color = Color.LightSalmon;
            }
            else if (selected != b.Selected)
            {
                b.Color = Color.White;
            }

            // Select a character when you click on the button
            if (mState.LeftButton == ButtonState.Pressed &&
                prevMState.LeftButton != ButtonState.Pressed &&
                b.Rect.Contains(mState.Position))
            {
                b.Color = Color.Salmon;

                // Update the character the user is examining
                selected = b.Selected;
            }
        }
    }
}
