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
        static float RES_SCALE_FACTOR_IN_WINDOW_MODE = 0.65f;
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

        public void toggleFullScreen()
        {
            if (this.graphics.IsFullScreen)
            {
                this.graphics.PreferredBackBufferHeight = (int)(1080 * RES_SCALE_FACTOR_IN_WINDOW_MODE);
                this.graphics.PreferredBackBufferWidth = (int)(1920 * RES_SCALE_FACTOR_IN_WINDOW_MODE);
            }
            else
            {
                System.Drawing.Rectangle bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                this.graphics.PreferredBackBufferHeight = bounds.Height;
                this.graphics.PreferredBackBufferWidth = bounds.Width;
            }
            this.graphics.ToggleFullScreen();
        }

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Player = new Player();
        }

        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferHeight = (int)(1080 * RES_SCALE_FACTOR_IN_WINDOW_MODE);
            this.graphics.PreferredBackBufferWidth = (int)(1920 * RES_SCALE_FACTOR_IN_WINDOW_MODE);
            this.graphics.ApplyChanges();
            Player.Stats.Health = 50;
            UI.Initialize(GraphicsDevice);
            KeyboardHandler.StartKeyboardHandler();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Player.LoadContent(this.Content, this.GraphicsDevice.Viewport.Width / 2, this.GraphicsDevice.Viewport.Height / 2);
            UI.LoadContent(this.Content);
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

            if (CurrState.IsKeyDown(Keys.Escape))
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            if(CurrState.IsKeyDown(Keys.F11))
                toggleFullScreen();

            Player.Update(gameTime);

            if(CurrState.IsKeyDown(Keys.Add))
                ++Player.Stats.Health;

            if (CurrState.IsKeyDown(Keys.Subtract))
                --Player.Stats.Health;

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
            UI.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
