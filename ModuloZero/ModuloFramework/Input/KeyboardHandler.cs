using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework.Input;

namespace ModuloFramework.Input
{
    /// <summary>
    /// A static class that handles key presses
    /// </summary>
    public class KeyboardHandler : IKeyboardHandler
    {
        private static volatile KeyboardHandler _instance;
        private static readonly object SyncObject = new object();

        public static KeyboardHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncObject)
                    {
                        if (_instance == null)
                            _instance = new KeyboardHandler();
                    }
                }

                return _instance;
            }
        }

        private Dictionary<Keys, KeyPressEventHandler> KeypressHandlers { get; set; }
        private KeyboardState Curr { get; set; }
        private KeyboardState Prev { get; set; }

        /// <summary>
        /// Initialize members
        /// </summary>
        private KeyboardHandler()
        {
            KeypressHandlers = new Dictionary<Keys, KeyPressEventHandler>();
            Prev = Keyboard.GetState();
            Curr = Keyboard.GetState();
        }

        /// <summary>
        /// Tries to invoke a KeyPressEventHandler
        /// </summary>
        /// <param name="handler">Handler to invoke</param>
        private static void OnKeyPressed(KeyPressEventHandler handler)
        {
            handler?.Invoke();
        }

        /// <summary>
        /// Subscribes a method to a certain key's keypress handler
        /// </summary>
        /// <param name="key">Microsoft.XNA.Framework.Input.Keys enum</param>
        /// <param name="method">a void() method</param>
        public void SubscribeToKeyPressEvent(Keys key, KeyPressEventHandler method)
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
        public void UnsubscribeToKeyPressEvent(Keys key, KeyPressEventHandler method)
        {
            if (KeypressHandlers.ContainsKey(key))
                KeypressHandlers[key] -= method;
        }

        /// <summary>
        /// Returns whether is key was just pressed
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>true if the key was just pressed; otherwise false</returns>
        public bool IsKeyPressedOnce(Keys key)
        {
            return Curr.IsKeyDown(key) && !Prev.IsKeyDown(key);
        }

        /// <summary>
        /// Starts the thread that handles activating key press methods
        /// </summary>
        public void Initialize()
        {
            Thread keyboardHandlingThread = new Thread(ThreadMethod);
            keyboardHandlingThread.Start();
        }

        /// <summary>
        /// constantly activates methods that are subscribed to key presses in the KeypressHandlers dictionary
        /// </summary>
        private void ThreadMethod()
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
    }
}
