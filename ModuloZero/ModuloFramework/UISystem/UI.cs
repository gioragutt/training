using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ModuloFramework.UISystem
{
    public delegate void UIDrawEventHandler(SpriteBatch spriteBatch);

    public static class UI
    {
        private static Dictionary<Color, Texture2D> ColorTextureRepository { get; set; }

        public static Texture2D GetColorTexture(Color color, GraphicsDevice graphicsDevice)
        {
            if (ColorTextureRepository.ContainsKey(color))
                return ColorTextureRepository[color];
            Texture2D tex = new Texture2D(graphicsDevice, 1, 1);
            tex.SetData<Color>(new Color[] { color });
            ColorTextureRepository[color] = tex;
            return tex;
        }

        public static SpriteFont Font { get; set; }

        public static Texture2D PlayerHealthTexture { get; set; }

        public static event UIDrawEventHandler DrawingEvent;

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
            ColorTextureRepository = new Dictionary<Color, Texture2D>();
        }

        public static void LoadContent(ContentManager manager)
        {
            Font = manager.Load<SpriteFont>("UI/MainFont");
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch != null)
                DrawingEvent?.Invoke(spriteBatch);
        }
    }
}
