using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace MonoGameFirst.BaseGameClasses
{
    public delegate void KeyPressEventHandler();

    /// <summary>
    /// A static class that handles key presses
    /// </summary>
    public static class KeyboardHandler
    {
        #region Data Members

        private static readonly Dictionary<Keys, KeyPressEventHandler> KeypressHandlers;

        private static KeyboardState Curr { get; set; }
        private static KeyboardState Prev { get; set; }

        #endregion

        #region Constructors

        static KeyboardHandler()
        {
            KeypressHandlers = new Dictionary<Keys, KeyPressEventHandler>();
            Prev = Keyboard.GetState();
            Curr = Keyboard.GetState();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to activate a KeyPressEventHandler method
        /// </summary>
        /// <param name="handler"></param>
        private static void OnKeyPressed(KeyPressEventHandler handler)
        {
            handler?.Invoke();
        }

        /// <summary>
        /// Subscribes a method to a certain key's keypress handler
        /// </summary>
        /// <param name="key">Microsoft.XNA.Framework.Input.Keys enum</param>
        /// <param name="method">a void() method</param>
        public static void SubscribeToKeyPressEvent(Keys key, KeyPressEventHandler method)
        {
            if (!KeypressHandlers.ContainsKey(key))
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

        public static bool IsKeyPressedOnce(Keys key)
        {
            return Curr.IsKeyDown(key) && !Prev.IsKeyDown(key);
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
            while (true)
            {
                Curr = Keyboard.GetState();
                foreach (Keys key in KeypressHandlers.Keys.Where(key => Curr.IsKeyDown(key)))
                {
                    OnKeyPressed(KeypressHandlers[key]);
                }

                Thread.Sleep(16);
                Prev = Curr;
            }
        }

        #endregion
    }
}
