using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModuloFramework.AbilitySystem;
using ModuloFramework.InputSystem;
using ModuloFramework.ItemSystem;
using ModuloFramework.StatSystem;
using ModuloFramework.UISystem;
using ModuloZero.BaseGameClasses.Player_Classes;

namespace ModuloZero
{
    public class DummyUnit : IUnit
    {
        private static IUnit _instance;

        public static IUnit Get()
        {
            return _instance ?? (_instance = new DummyUnit());
        }
    }

    public class MainGame : Game
    {
        public const float RES_SCALE_FACTOR_IN_WINDOW_MODE = 0.65f;
        public GraphicsDeviceManager Graphics { get; }
        private SpriteBatch spriteBatch;
        private readonly VariableStat number;
        private readonly DependentVariableStat dependentNumber;
        private readonly TestItem x;

        private KeyboardState currState;
        private KeyboardState prevState;

        public Player Player { get; set; }

        public MainGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Player = new Player();
            number = new VariableStat.AsInt(10);
            dependentNumber = new DependentVariableStat(10, number);
            x = new TestItem();
        }

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
            UI.Instance.Initialize(GraphicsDevice);
            Player.Initialize(UI.Instance);
            KeyboardHandler.Instance.Initialize();
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
            UI.Instance.LoadContent(Content);
        }

        protected override void UnloadContent() => this.Content.Unload();

        protected override void Update(GameTime gameTime)
        {
            currState = Keyboard.GetState();

            if (currState.IsKeyDown(Keys.Escape))
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            if (currState.IsKeyDown(Keys.F11))
                ToggleFullScreen();

            if (!currState.IsKeyDown(Keys.LeftControl))
            {
                if (currState.IsKeyDown(Keys.Add))
                    number.AddRawBonus(new RawBonus(10));

                if (currState.IsKeyDown(Keys.Subtract))
                    number.RemoveRawBonus(new RawBonus(10));
            }
            else
            {
                if (currState.IsKeyDown(Keys.Add))
                    dependentNumber.AddRawBonus(new RawBonus(10));

                if (currState.IsKeyDown(Keys.Subtract))
                    dependentNumber.RemoveRawBonus(new RawBonus(10));
            }

            Player.Update(gameTime);

            if (currState.IsKeyDown(Keys.Space) && !prevState.IsKeyDown(Keys.Space))
                Player.ToggleSubscriptionToKeyboardHandler();
            if (currState.IsKeyDown(Keys.X) && !prevState.IsKeyDown(Keys.X))
                x.Item1.Activate(DummyUnit.Get());

            base.Update(gameTime);
            prevState = currState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.DrawString(UI.Instance.DefaultFont, number.FinalValue.ToString(CultureInfo.InvariantCulture),
                new Vector2(20, 20), Color.Black);
            spriteBatch.DrawString(UI.Instance.DefaultFont, dependentNumber.FinalValue.ToString(CultureInfo.InvariantCulture),
                new Vector2(20, 35), Color.Black);
            spriteBatch.DrawString(UI.Instance.DefaultFont,
                x.Item1.Ability.RemainingCooldown.TotalMilliseconds.ToString(CultureInfo.CurrentCulture),
                new Vector2(20, 65), Color.Black);
            spriteBatch.DrawString(UI.Instance.DefaultFont, x.Item1.Description, new Vector2(20, 80), Color.Black);
            if (x.Item1.Ability.Cooldown != null)
            {
                Rectangle cooldown = new Rectangle(15, 53, 350,
                    (int)
                        (70 *
                         (x.Item1.Ability.RemainingCooldown.TotalMilliseconds /
                          x.Item1.Ability.Cooldown.Value.TotalMilliseconds)));
                spriteBatch.Draw(UI.Instance.GetColorTexture(Color.Black, GraphicsDevice), cooldown, Color.White * 0.1f);
            }

            Player.Draw(spriteBatch);
            UI.Instance.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
