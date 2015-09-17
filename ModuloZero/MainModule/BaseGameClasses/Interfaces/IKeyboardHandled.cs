/*
    Note: When subscribing to keyboard presses,
    There is a difference when changing order of subscriptions
    F.E
    void x() { position.x = 3; }
    void y() { position.x = 5; }
    KB.SUB(Keys.A, x)
    KB.SUB(Keys.B, y)
    Pressing both keys will set position.x first to 3 and later to 5, thus showing 5
    and vice versa, first to 5 and later to 3, showing 3
*/

namespace MonoGameFirst.BaseGameClasses.Interfaces
{
    public interface IKeyboardHandled
    {
        void SubscribeToKeyboardHandler();
        void UnubscribeFromKeyboardHandler();
        void ToggleSubscriptionToKeyboardHandler(bool toSubscribe = true);
    }
}
