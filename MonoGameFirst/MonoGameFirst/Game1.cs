using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameFirst.BaseGameClasses;
using System;

namespace MonoGameFirst
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region XNA

        KeyboardState CurrState;
        KeyboardState PrevState;

        #endregion

        #region Content

        public Player Player
        {
            get; set;
        }

        #endregion

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            foreach (Keys x in Enum.GetValues(typeof(Keys)))
            {

            }
        }

        protected override void Initialize()
        {
            Player = new Player();
            KeyboardHandler.StartKeyboardHandler();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Player.LoadSprite(this.Content.Load<Texture2D>(@"player_image"), 10, 10);
        }

        protected override void UnloadContent()
        {
            this.Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            CurrState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || CurrState.IsKeyDown(Keys.Escape))
                Exit();

            if (CurrState.IsKeyDown(Keys.Space) && !PrevState.IsKeyDown(Keys.Space))
                Player.ToggleSubscriptionToKeyboardHandler();

            base.Update(gameTime);
            PrevState = CurrState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
