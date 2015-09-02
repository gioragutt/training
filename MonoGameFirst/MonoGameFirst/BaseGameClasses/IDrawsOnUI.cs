using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameFirst.BaseGameClasses
{
    interface IDrawsOnUI
    {
        void UIDraw(SpriteBatch spriteBatch);
    }
}
