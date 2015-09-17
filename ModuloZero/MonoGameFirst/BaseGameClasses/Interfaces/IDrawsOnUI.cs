using Microsoft.Xna.Framework.Graphics;

namespace MonoGameFirst.BaseGameClasses.Interfaces
{
    interface IDrawsOnUI
    {
        void UIDraw(SpriteBatch spriteBatch);
        bool ShouldDraw { get; set; }
    }
}
