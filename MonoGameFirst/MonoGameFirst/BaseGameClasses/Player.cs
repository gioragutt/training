using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameFirst.BaseGameClasses
{
    public class Player : IKeyboardHandled
    {
        #region Properties

        /// <summary>
        /// Gets and sets the ID of the player (for future multiplayer needs)
        /// </summary>
        public int ID
        {
            get; set;
        }

        /// <summary>
        /// Stats of the player
        /// </summary>
        public PlayerStats Stats
        {
            get; private set;
        }

        /// <summary>
        /// Gets whether the player is subscibed to the keyboard handler
        /// </summary>
        public bool IsSubscribedToKeyboardHandler
        {
            get;
            private set;
        }

        /// <summary>
        /// The sprite of the player
        /// </summary>
        public Sprite Sprite
        {
            get; set;
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

        #region Basic methods (that subscribe to Keyboard handler)

        private void MoveUp()
        {
            Sprite.Y -= Stats.MoveSpeed;
        }
        private void MoveDown()
        {
            Sprite.Y += Stats.MoveSpeed;
        }
        private void MoveLeft()
        {
            Sprite.X -= Stats.MoveSpeed;
        }
        private void MoveRight()
        {
            Sprite.X += Stats.MoveSpeed;
        }

        #endregion

        /// <summary>
        /// Subscribes the player to the keyboard handler
        /// </summary>
        public void SubscribeToKeyboardHandler()
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

        /// <summary>
        /// Unubscribe the player from the keyboard handler
        /// </summary>
        public void UnubscribeFromKeyboardHandler()
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

        public void LoadContent(ContentManager manager, string assetName, int starting_x_pos, int starting_y_pos)
        {
            Sprite = new Sprite(manager.Load<Texture2D>(assetName), starting_x_pos, starting_y_pos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }

        #endregion
    }
}
