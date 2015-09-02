using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MonoGameFirst.BaseGameClasses
{
    public delegate void UIDrawEventHandler(SpriteBatch spriteBatch);

    public static class UI
    {
        #region Properties

        public static SpriteFont Font
        { get; set; }

        public static Texture2D PlayerHealthTexture
        { get; set; }

        public static event UIDrawEventHandler DrawingEvent;

        #endregion

        #region Methods

        public static void SubscribeToUIDraw(UIDrawEventHandler handler)
        {
            DrawingEvent += handler;
        }

        public static void UnsubscribeFromUIDraw(UIDrawEventHandler handler)
        {
            DrawingEvent -= handler;
        }

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            PlayerHealthTexture = new Texture2D(graphicsDevice, 1, 1);
            PlayerHealthTexture.SetData<Color>(new Color[] { Color.Red });
        }

        public static void LoadContent(ContentManager manager)
        {
            Font = manager.Load<SpriteFont>("UI/MainFont");
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            DrawingEvent(spriteBatch);
        }

        #endregion

    }
}
