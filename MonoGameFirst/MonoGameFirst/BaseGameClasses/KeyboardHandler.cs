using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace MonoGameFirst.BaseGameClasses
{
    /// <summary>
    /// Singleton class that handles key presses
    ///     - Subscribe to a keypress using SubscribeToKeyPressEvent()
    ///     - Unsubscribe from a keypress using UnsubscribeToKeyPressEvent()
    /// </summary>
    public class KeyboardHandler
    {
        #region Data Members

        public delegate void KeyPressEventHandler();
        private static KeyboardHandler instance;
        public Dictionary<Keys, KeyPressEventHandler> KeypressHandlers;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize the static instance (singleton)
        /// </summary>
        static KeyboardHandler()
        {
            instance = new KeyboardHandler();
        }

        /// <summary>
        /// Initialize the KeypressHandlers dictionary
        /// </summary>
        private KeyboardHandler()
        {
            this.KeypressHandlers = new Dictionary<Keys, KeyPressEventHandler>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to activate a KeyPressEventHandler method
        /// </summary>
        /// <param name="handler"></param>
        public void OnKeyPressed(KeyPressEventHandler handler)
        {
            if (handler != null)
                handler();
        }


        /// <summary>
        /// Subscribes a method to a certain key's keypress handler
        /// </summary>
        /// <param name="key">Microsoft.XNA.Framework.Input.Keys enum</param>
        /// <param name="method">a void() method</param>
        public static void SubscribeToKeyPressEvent(Keys key, KeyPressEventHandler method)
        {
            instance.SubscribeToKeyPressEventImpl(key, method);
        }
        public void SubscribeToKeyPressEventImpl(Keys key, KeyPressEventHandler method)
        {
            if(!this.KeypressHandlers.ContainsKey(key))
                this.KeypressHandlers[key] = method;
            else
                this.KeypressHandlers[key] += method;
        }


        /// <summary>
        /// Ubsubscribes a method from a certain key's keypress handler
        /// </summary>
        /// <param name="key">Microsoft.XNA.Framework.Input.Keys enum</param>
        /// <param name="method">a void() method</param>
        public static void UnsubscribeToKeyPressEvent(Keys key, KeyPressEventHandler method)
        {
            instance.UnsubscribeToKeyPressEventImpl(key, method);
        }
        public void UnsubscribeToKeyPressEventImpl(Keys key, KeyPressEventHandler method)
        {
            if (this.KeypressHandlers.ContainsKey(key))
                this.KeypressHandlers[key] -= method;
        }

        #endregion

        #region Thread Methods

        /// <summary>
        /// Starts the thread that handles activating key press methods
        /// </summary>
        public static void StartKeyboardHandler()
        {
            Thread keyboardHandlingThread = new Thread(instance.ThreadMethod);
            keyboardHandlingThread.Start();
        }

        /// <summary>
        /// constantly activates methods that are subscribed to key presses in the KeypressHandlers dictionary
        /// </summary>
        private void ThreadMethod()
        {
            KeyboardState state = Keyboard.GetState();
            while (true)
            {
                state = Keyboard.GetState();

                foreach (Keys key in this.KeypressHandlers.Keys)
                {
                    if (state.IsKeyDown(key))
                        OnKeyPressed(this.KeypressHandlers[key]);
                }

                Thread.Sleep(10);
            }
        }

        #endregion
    }
}
