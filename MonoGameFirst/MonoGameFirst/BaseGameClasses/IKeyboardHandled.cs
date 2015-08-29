using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameFirst.BaseGameClasses
{
    public interface IKeyboardHandled
    {
        void SubscribeToKeyboardHandler();
        void UnubscribeFromKeyboardHandler();
        void ToggleSubscriptionToKeyboardHandler(bool toSubscribe = true);
    }
}
