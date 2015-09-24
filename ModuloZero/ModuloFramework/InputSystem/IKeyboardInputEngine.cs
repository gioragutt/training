using Microsoft.Xna.Framework.Input;

namespace ModuloFramework.InputSystem
{
    public delegate void KeyPressEventHandler();

    public interface IKeyboardInputEngine
    {
        /// <summary>
        /// Subscribe a function to a key press
        /// </summary>
        /// <param name="key">Microsoft.Xna.Framework.Input.Keys</param>
        /// <param name="method">The function (of type void()) to be invoked on key press</param>
        void SubscribeToKeyPressEvent(Keys key, KeyPressEventHandler method);

        /// <summary>
        /// Unsubscribe a function from a key press
        /// </summary>
        /// <param name="key">Microsoft.Xna.Framework.Input.Keys</param>
        /// <param name="method">The function (of type void()) to be invoked on key press</param>
        void UnsubscribeToKeyPressEvent(Keys key, KeyPressEventHandler method);

        /// <summary>
        /// Returns whether a key is pressed(without further holding of the key)
        /// </summary>
        /// <param name="key">Microsoft.Xna.Framework.Input.Keys</param>
        /// <returns>true if the button is pressed; otherwise false;</returns>
        bool IsKeyPressedOnce(Keys key);
    }
}
