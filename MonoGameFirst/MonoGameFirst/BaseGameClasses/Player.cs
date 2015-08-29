using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameFirst.BaseGameClasses
{
    public class Player
    {
        #region Properties

        public PlayerStats Stats
        {
            get; set;
        }

        public int ID
        {
            get; set;
        }

        public Sprite Sprite
        {
            get; set;
        }

        public bool IsSubscribedToKeyboardHandler
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public Player()
        {
            Stats = new PlayerStats()
            {
                Health = 100,
                MoveSpeed = 3
            };

            SubscribeToKeyboardHandler();
        }

        #endregion

        #region Private Methods

        private void MoveUp()
        {
            Sprite.Rectangle.Y -= Stats.MoveSpeed;
        }
        private void MoveDown()
        {
            Sprite.Rectangle.Y += Stats.MoveSpeed;
        }
        private void MoveLeft()
        {
            Sprite.Rectangle.X -= Stats.MoveSpeed;
        }
        private void MoveRight()
        {
            Sprite.Rectangle.X += Stats.MoveSpeed;
        }

        private void SubscribeToKeyboardHandler()
        {
            if (IsSubscribedToKeyboardHandler == false)
            {
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.Down, MoveDown);
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.Up, MoveUp);
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.Right, MoveRight);
                KeyboardHandler.SubscribeToKeyPressEvent(Keys.Left, MoveLeft);
            }
            IsSubscribedToKeyboardHandler = true;
        }

        private void UnubscribeFromKeyboardHandler()
        {
            if (IsSubscribedToKeyboardHandler == true)
            {
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.Down, MoveDown);
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.Up, MoveUp);
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.Right, MoveRight);
                KeyboardHandler.UnsubscribeToKeyPressEvent(Keys.Left, MoveLeft);
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

        #region Public Methods

        public void LoadSprite(Texture2D texture, int starting_x_pos, int starting_y_pos)
        {
            Sprite = new Sprite(texture, starting_x_pos, starting_y_pos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }

        #endregion
    }
}
