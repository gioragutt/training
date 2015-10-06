using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TutorialRPG
{
    internal class InputManager
    {
        /// <summary>
        /// .Net Lazy object for singleton
        /// </summary>
        private static readonly Lazy<InputManager> Lazy =
            new Lazy<InputManager>(() => new InputManager());

        /// <summary>
        /// Returns the singleton instance of the InputManager
        /// </summary>
        public static InputManager Instance
        {
            get { return Lazy.Value; }
        }

        public KeyboardState CurrentState { get; private set; }
        public KeyboardState PreviousState { get; private set; }

        /// <summary>
        /// Initializes members of the InputManager class, private for singleton
        /// </summary>
        private InputManager() { }

        public void Update()
        {
            PreviousState = CurrentState;
            if (!ScreenManager.Instance.IsTransitioning)
                CurrentState = Keyboard.GetState();
        }

        public bool KeyPressed(params Keys[] keys)
        {
            return keys.Any(key => CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key));
        }

        public bool KeyReleased(params Keys[] keys)
        {
            return keys.Any(key => CurrentState.IsKeyUp(key) && PreviousState.IsKeyDown(key));
        }

        public bool KeyDown(params Keys[] keys)
        {
            return keys.Any(key => CurrentState.IsKeyDown(key));
        }
    }
}
