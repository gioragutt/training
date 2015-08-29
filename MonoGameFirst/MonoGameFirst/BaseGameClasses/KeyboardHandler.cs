using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace MonoGameFirst.BaseGameClasses
{
    public delegate void KeyPressEventHandler();

    /// <summary>
    /// A not-really Singleton class that handles key presses
    ///     - Subscribe to a keypress using SubscribeToKeyPressEvent()
    ///     - Unsubscribe from a keypress using UnsubscribeToKeyPressEvent()
    /// </summary>
    public static class KeyboardHandler
    {
        #region Data Members

        private static Dictionary<Keys, KeyPressEventHandler> KeypressHandlers;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize the dictionary
        /// </summary>
        static KeyboardHandler()
        {
            KeypressHandlers = new Dictionary<Keys, KeyPressEventHandler>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to activate a KeyPressEventHandler method
        /// </summary>
        /// <param name="handler"></param>
        private static void OnKeyPressed(KeyPressEventHandler handler)
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
            if(!KeypressHandlers.ContainsKey(key))
                KeypressHandlers[key] = method;
            else
                KeypressHandlers[key] += method;
        }


        /// <summary>
        /// Ubsubscribes a method from a certain key's keypress handler
        /// </summary>
        /// <param name="key">Microsoft.XNA.Framework.Input.Keys enum</param>
        /// <param name="method">a void() method</param>
        public static void UnsubscribeToKeyPressEvent(Keys key, KeyPressEventHandler method)
        {
            if (KeypressHandlers.ContainsKey(key))
                KeypressHandlers[key] -= method;
        }

        #endregion

        #region Thread Methods

        /// <summary>
        /// Starts the thread that handles activating key press methods
        /// </summary>
        public static void StartKeyboardHandler()
        {
            Thread keyboardHandlingThread = new Thread(ThreadMethod);
            keyboardHandlingThread.Start();
        }

        /// <summary>
        /// constantly activates methods that are subscribed to key presses in the KeypressHandlers dictionary
        /// </summary>
        private static void ThreadMethod()
        {
            KeyboardState state = Keyboard.GetState();
            while (true)
            {
                state = Keyboard.GetState();

                foreach (Keys key in KeypressHandlers.Keys)
                {
                    if (state.IsKeyDown(key))
                        OnKeyPressed(KeypressHandlers[key]);
                }

                Thread.Sleep(10);
            }
        }

        #endregion
    }
}
