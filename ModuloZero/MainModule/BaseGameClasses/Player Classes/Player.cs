using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModuloFramework.Input;
using ModuloFramework.UISystem;
using MonoGameFirst.BaseGameClasses.Interfaces;

namespace ModuloZero.BaseGameClasses.Player_Classes
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public class Player : IKeyboardHandled, IDrawsOnUI
    {
        private Vector2 currentFrame;

        public bool ShouldDraw { get; set; }
        public int ID { get; set; }
        private bool IsSubscribedToKeyboardHandler { get; set; }

        /// <summary>
        /// The sprite of the player
        /// </summary>
        public Sprite Sprite { get; set; }

        /// <summary>
        /// Gets the spritesheet of the character
        /// </summary>
        public Texture2D[,] SpriteSheet { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private Direction Direction { get; set; }

        private int FrameCounter { get; set; }

        private bool IsAnimationActive { get; set; }

        public Player()
        {
            FrameCounter = 0;
            ShouldDraw = true;
        }

        private void CommonAfterMovement()
        {
            IsAnimationActive = true;
        }

        private void UpMovement()
        {
            MoveUpImpl();
            ChangeAnimToUp();
            CommonAfterMovement();
        }

        private void DownMovement()
        {
            MoveDownImpl();
            ChangeAnimToDown();
            CommonAfterMovement();
        }

        private void LeftMovement()
        {
            MoveLeftImpl();
            ChangeAnimToLeft();
            CommonAfterMovement();
        }

        private void RightMovement()
        {
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

        private void MoveUpImpl() { }

        private void MoveDownImpl() { }

        private void MoveLeftImpl() { }

        private void MoveRightImpl() { }

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

        /// <summary>
        /// Subscribes the player to the keyboard handler
        /// </summary>
        public void SubscribeToKeyboardHandler()
        {
            if (IsSubscribedToKeyboardHandler == false)
            {
                KeyboardHandler.Instance.SubscribeToKeyPressEvent(Keys.W, UpMovement);
                KeyboardHandler.Instance.SubscribeToKeyPressEvent(Keys.S, DownMovement);
                KeyboardHandler.Instance.SubscribeToKeyPressEvent(Keys.A, LeftMovement);
                KeyboardHandler.Instance.SubscribeToKeyPressEvent(Keys.D, RightMovement);
                KeyboardHandler.Instance.SubscribeToKeyPressEvent(Keys.Down, AttackDown);
                KeyboardHandler.Instance.SubscribeToKeyPressEvent(Keys.Up, AttackUp);
                KeyboardHandler.Instance.SubscribeToKeyPressEvent(Keys.Right, AttackRight);
                KeyboardHandler.Instance.SubscribeToKeyPressEvent(Keys.Left, AttackLeft);
            }
            IsSubscribedToKeyboardHandler = true;
        }

        /// <summary>
        /// Unubscribe the player from the keyboard handler
        /// </summary>
        public void UnubscribeFromKeyboardHandler()
        {
            if (IsSubscribedToKeyboardHandler)
            {
                KeyboardHandler.Instance.UnsubscribeToKeyPressEvent(Keys.Down, AttackDown);
                KeyboardHandler.Instance.UnsubscribeToKeyPressEvent(Keys.Up, AttackUp);
                KeyboardHandler.Instance.UnsubscribeToKeyPressEvent(Keys.Right, AttackRight);
                KeyboardHandler.Instance.UnsubscribeToKeyPressEvent(Keys.Left, AttackLeft);
                KeyboardHandler.Instance.UnsubscribeToKeyPressEvent(Keys.W, UpMovement);
                KeyboardHandler.Instance.UnsubscribeToKeyPressEvent(Keys.S, DownMovement);
                KeyboardHandler.Instance.UnsubscribeToKeyPressEvent(Keys.A, LeftMovement);
                KeyboardHandler.Instance.UnsubscribeToKeyPressEvent(Keys.D, RightMovement);
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
                SubscribeToKeyboardHandler();
            else
                UnubscribeFromKeyboardHandler();
        }

        private void SetCurrentFrame(Vector2 frameToSet)
        {
            Sprite.Texture = SpriteSheet[(int)frameToSet.X, (int)frameToSet.Y];
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            if (IsAnimationActive)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter >= 100)
                {
                    FrameCounter = 0;
                    currentFrame.Y = (currentFrame.Y + 1) % SpriteSheet.GetLength(1);
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

        public void Initialize()
        {
            UI.SubscribeToUIDraw(UIDraw);
            SubscribeToKeyboardHandler();
        }

        public void LoadContent(ContentManager manager, int startingXPos, int startingYPos)
        {
            Func<string, Texture2D> loadText = manager.Load<Texture2D>;
            SpriteSheet = new Texture2D[,]
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
            Sprite = new Sprite(SpriteSheet[(int)currentFrame.X, (int)currentFrame.Y], startingXPos, startingYPos);
        }

        public void Update(GameTime gameTime)
        {
            UpdateAnimation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, scale: 0.4f);
        }

        public void UIDraw(SpriteBatch spriteBatch)
        {
            //if (!ShouldDraw)
            //    return;
            //int startingWidth;
            //Vector2 hpFontSize;
            //using (Texture2D healthBarBackgroundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1))
            //{
            //    healthBarBackgroundTexture.SetData(new[] { Color.AntiqueWhite });
            //    startingWidth = spriteBatch.GraphicsDevice.Viewport.Width / 2 - 100;
            //    string hpString = string.Format("HP:{0}", Stats.Health.ToString().PadLeft(4));
            //    hpFontSize = UI.Font.MeasureString(hpString);
            //    spriteBatch.DrawString(UI.Font, hpString, new Vector2(startingWidth, 10f), Color.Black);
            //    spriteBatch.Draw(healthBarBackgroundTexture,
            //        new Rectangle(startingWidth + 5 + (int)hpFontSize.X, 10, 150, (int)hpFontSize.Y), Color.White);
            //}
            //spriteBatch.Draw(UI.PlayerHealthTexture, new Rectangle(startingWidth + 5 + (int)hpFontSize.X, 10,
            //    (int)(150f * PercentHealth), (int)hpFontSize.Y), Color.Red);
        }
    }
}
