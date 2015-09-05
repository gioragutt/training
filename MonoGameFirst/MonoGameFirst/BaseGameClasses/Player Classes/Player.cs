using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameFirst.BaseGameClasses.Interfaces;
using MonoGameFirst.BaseGameClasses.Item_System;

namespace MonoGameFirst.BaseGameClasses.Player_Classes
{
    internal enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public class Player : IKeyboardHandled, IDrawsOnUI
    {
        #region Data Members

        private Vector2 currentFrame;

        #endregion

        #region Properties

        public bool ShouldDraw { get; set; }

        /// <summary>
        /// Gets and sets the ID of the player (for future multiplayer needs)
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Stats of the player
        /// </summary>
        public PlayerStats Stats { get; private set; }

        /// <summary>
        /// Gets whether the player is subscibed to the keyboard handler
        /// </summary>
        public bool IsSubscribedToKeyboardHandler { get; private set; }

        public Inventory Inventory { get; set; }

        #region Animation Related Properties

        /// <summary>
        /// The sprite of the player
        /// </summary>
        public Sprite Sprite { get; set; }

        /// <summary>
        /// Gets the spritesheet of the character
        /// </summary>
        public Texture2D[,] SpriteSheet { get; set; }

        private Direction Direction { get; set; }

        private Vector2 PreviousPosition { get; set; }

        private int FrameCounter { get; set; }

        private bool IsAnimationActive { get; set; }

        #endregion

        #endregion

        #region Constructor

        public Player()
        {
            FrameCounter = 0;
            Stats = new PlayerStats()
            {
                MaxHealth = 100,
                Health = 50,
                MoveSpeed = 5
            };

            ShouldDraw = true;

            Inventory = new Inventory(this);
            UI.SubscribeToUIDraw(UIDraw);
            SubscribeToKeyboardHandler();
        }

        #endregion

        #region Basic Methods

        #region Methods that are subscribed

        private void CommonBeforeMovement()
        {
            PreviousPosition = Sprite.Position;
        }

        private void CommonAfterMovement()
        {
            IsAnimationActive = true;
        }

        private void UpMovement()
        {
            CommonBeforeMovement();
            MoveUpImpl();
            ChangeAnimToUp();
            CommonAfterMovement();
        }

        private void DownMovement()
        {
            CommonBeforeMovement();
            MoveDownImpl();
            ChangeAnimToDown();
            CommonAfterMovement();
        }

        private void LeftMovement()
        {
            CommonBeforeMovement();
            MoveLeftImpl();
            ChangeAnimToLeft();
            CommonAfterMovement();
        }

        private void RightMovement()
        {
            CommonBeforeMovement();
            MoveRightImpl();
            ChangeAnimToRight();
            CommonAfterMovement();
        }

        private void AttackLeft()
        {
            ChangeAnimToLeft();
            Direction = Direction.Left;
        }

        private void AttackRight()
        {
            ChangeAnimToRight();
            Direction = Direction.Right;
        }

        private void AttackUp()
        {
            ChangeAnimToUp();
            Direction = Direction.Up;
        }

        private void AttackDown()
        {
            ChangeAnimToDown();
            Direction = Direction.Down;
        }

        #endregion

        #region Implentations that go inside the methods that are subscribed

        private void MoveUpImpl()
        {
            Sprite.Y -= Stats.MoveSpeed;
        }

        private void MoveDownImpl()
        {
            Sprite.Y += Stats.MoveSpeed;
        }

        private void MoveLeftImpl()
        {
            Sprite.X -= Stats.MoveSpeed;
        }

        private void MoveRightImpl()
        {
            Sprite.X += Stats.MoveSpeed;
        }

        private void ChangeAnimToUp()
        {
            currentFrame.X = 0;
        }

        private void ChangeAnimToDown()
        {
            currentFrame.X = 2;
        }

        private void ChangeAnimToLeft()
        {
            currentFrame.X = 3;
        }

        private void ChangeAnimToRight()
        {
            currentFrame.X = 1;
        }

        #endregion

        #endregion

        #region IKeyboardHandled Methods

        /// <summary>
        /// Subscribes the player to the keyboard handler
        /// </summary>
        public void SubscribeToKeyboardHandler()
        {
            if (IsSubscribedToKeyboardHandler == false)
            {
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.W, UpMovement);
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.S, DownMovement);
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.A, LeftMovement);
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.D, RightMovement);
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.Down, AttackDown);
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.Up, AttackUp);
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.Right, AttackRight);
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.Left, AttackLeft);
            }
            IsSubscribedToKeyboardHandler = true;
        }

        /// <summary>
        /// Unubscribe the player from the keyboard handler
        /// </summary>
        public void UnubscribeFromKeyboardHandler()
        {
            if (IsSubscribedToKeyboardHandler == true)
            {
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.Down, AttackDown);
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.Up, AttackUp);
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.Right, AttackRight);
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.Left, AttackLeft);
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.W, UpMovement);
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.S, DownMovement);
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.A, LeftMovement);
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.D, RightMovement);
            }
            IsSubscribedToKeyboardHandler = false;
        }

        /// <summary>
        /// Toggles subscription to keyboard handler
        /// </summary>
        /// <param name="toSubscribe">Defaults to true; if should subscribe</param>
        public void ToggleSubscriptionToKeyboardHandler(bool toSubscribe = true)
        {
            if (!IsSubscribedToKeyboardHandler && toSubscribe)
                this.SubscribeToKeyboardHandler();
            else
                this.UnubscribeFromKeyboardHandler();
        }

        #endregion

        #region Animation Methods

        private void SetCurrentFrame(Vector2 frameToSet)
        {
            Sprite.Texture = SpriteSheet[(int) frameToSet.X, (int) frameToSet.Y];
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            if (IsAnimationActive)
            {
                FrameCounter += (int) gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter >= 60)
                {
                    FrameCounter = 0;
                    currentFrame.Y = (currentFrame.Y + 1)%SpriteSheet.GetLength(1);
                }
            }
            else
            {
                FrameCounter = 0;
                currentFrame.Y = 0;
            }
            SetCurrentFrame(currentFrame);
            IsAnimationActive = false;
        }

        #endregion

        #region XNA Methods

        public void Initialize()
        {
            Inventory.Initialize();
        }

        public void LoadContent(ContentManager manager, int startingXPos, int startingYPos)
        {
            Func<string, Texture2D> loadText = manager.Load<Texture2D>;
            SpriteSheet = new Texture2D[4, 4]
            {
                {
                    loadText("GotSprites/Back_2"), loadText("GotSprites/Back_1"), loadText("GotSprites/Back_2"),
                    loadText("GotSprites/Back_3")
                },
                {
                    loadText("GotSprites/Right_2"), loadText("GotSprites/Right_1"), loadText("GotSprites/Right_2"),
                    loadText("GotSprites/Right_3")
                },
                {
                    loadText("GotSprites/Front_2"), loadText("GotSprites/Front_1"), loadText("GotSprites/Front_2"),
                    loadText("GotSprites/Front_3")
                },
                {
                    loadText("GotSprites/Left_2"), loadText("GotSprites/Left_1"), loadText("GotSprites/Left_2"),
                    loadText("GotSprites/Left_3")
                }
            };
            currentFrame = new Vector2(2f, 0f);
            Sprite = new Sprite(SpriteSheet[(int) currentFrame.X, (int) currentFrame.Y], startingXPos, startingYPos);
        }

        public void Update(GameTime gameTime)
        {
            UpdateAnimation(gameTime);
            Inventory.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, scale: 0.4f);
        }

        public void UIDraw(SpriteBatch spriteBatch)
        {
            if (!ShouldDraw)
                return;
            int startingWidth = spriteBatch.GraphicsDevice.Viewport.Width/2 - 100;
            string hpString = string.Format("HP:{0}", this.Stats.Health.ToString().PadLeft(4));
            Vector2 hpFontSize = UI.Font.MeasureString(hpString);
            spriteBatch.DrawString(UI.Font, hpString, new Vector2((float) startingWidth, 10f), Color.Black);
            spriteBatch.Draw(UI.PlayerHealthTexture, new Rectangle(startingWidth + 5 + (int) hpFontSize.X, 10,
                             (int)(150f*this.Stats.PercentHealth), (int)hpFontSize.Y), Color.Red);
        }

        #endregion
    }
}
