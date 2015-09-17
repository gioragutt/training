using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameFirst.BaseGameClasses;
using MonoGameFirst.BaseGameClasses.Player_Classes;

namespace MonoGameFirst
{
    public class MainGame : Game
    {
        #region Data Members

        public const float RES_SCALE_FACTOR_IN_WINDOW_MODE = 0.65f;
        public GraphicsDeviceManager Graphics { get; }
        private SpriteBatch spriteBatch;

        #endregion

        #region XNA

        private KeyboardState currState;
        private KeyboardState prevState;

        #endregion

        #region Content

        public Player Player { get; set; }

        #endregion

        #region Constructor

        public MainGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Player = new Player();
        }

        #endregion

        #region Methods

        public void ToggleFullScreen()
        {
            if (this.Graphics.IsFullScreen)
            {
                this.Graphics.PreferredBackBufferHeight = (int)(1080 * RES_SCALE_FACTOR_IN_WINDOW_MODE);
                this.Graphics.PreferredBackBufferWidth = (int)(1920 * RES_SCALE_FACTOR_IN_WINDOW_MODE);
            }
            else
            {
                System.Drawing.Rectangle bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                this.Graphics.PreferredBackBufferHeight = bounds.Height;
                this.Graphics.PreferredBackBufferWidth = bounds.Width;
            }
            this.Graphics.ToggleFullScreen();
        }

        protected override void Initialize()
        {
            InitializeWindowSize();
            Player.Initialize();
            UI.Initialize(GraphicsDevice);
            KeyboardHandler.StartKeyboardHandler();
            base.Initialize();
        }

        private void InitializeWindowSize()
        {
            this.Graphics.PreferredBackBufferHeight = (int)(1080 * RES_SCALE_FACTOR_IN_WINDOW_MODE);
            this.Graphics.PreferredBackBufferWidth = (int)(1920 * RES_SCALE_FACTOR_IN_WINDOW_MODE);
            this.Graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Player.LoadContent(Content, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            UI.LoadContent(Content);
        }

        protected override void UnloadContent() => this.Content.Unload();

        protected override void Update(GameTime gameTime)
        {
            currState = Keyboard.GetState();

            if (currState.IsKeyDown(Keys.Escape))
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            if (currState.IsKeyDown(Keys.F11))
                ToggleFullScreen();

            Player.Update(gameTime);

            if (currState.IsKeyDown(Keys.Space) && !prevState.IsKeyDown(Keys.Space))
                Player.ToggleSubscriptionToKeyboardHandler();

            base.Update(gameTime);
            prevState = currState;
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

        #endregion
    }
}
