using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ModuloZero.BaseGameClasses
{
    public delegate void UIDrawEventHandler(SpriteBatch spriteBatch);

    public static class UI
    {
        public static Texture2D GetNewColorTexture(Color color, GraphicsDevice graphicsDevice)
        {
            Texture2D tex = new Texture2D(graphicsDevice, 1, 1);
            tex.SetData<Color>(new Color[] { color });
            return tex;
        }

        #region Properties

        public static SpriteFont Font { get; set; }

        public static Texture2D PlayerHealthTexture { get; set; }

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

        #endregion
    }
}
