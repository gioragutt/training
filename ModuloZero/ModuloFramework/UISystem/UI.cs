using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ModuloFramework.UISystem
{
    public sealed class UI : IDrawingEngine
    {
        private static volatile UI _instance;
        private static readonly object SyncObject = new object();

        public static UI Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncObject)
                    {
                        if (_instance == null)
                            _instance = new UI();
                    }
                }

                return _instance;
            }
        }

        private SpriteFont Font { get; set; }

        public GraphicsDevice GraphicsDevice { get; private set; }

        public event UIDrawEventHandler DrawingEvent;

        private UI()
        {
            ColorTextureRepository = new Dictionary<Color, Texture2D>();
        }

        private Dictionary<Color, Texture2D> ColorTextureRepository { get; set; }

        public Texture2D GetColorTexture(Color color, GraphicsDevice graphicsDevice)
        {
            if (ColorTextureRepository.ContainsKey(color))
                return ColorTextureRepository[color];
            Texture2D tex = new Texture2D(graphicsDevice, 1, 1);
            tex.SetData<Color>(new Color[] { color });
            ColorTextureRepository[color] = tex;
            return tex;
        }

        public SpriteFont DefaultFont
        {
            get { return Font; }
        }

        public Texture2D GetColorTexture(Color color)
        {
            return GetColorTexture(color, GraphicsDevice);
        }

        public void SubscribeToUIDraw(UIDrawEventHandler handler)
        {
            DrawingEvent += handler;
        }

        public void UnsubscribeFromUIDraw(UIDrawEventHandler handler)
        {
            DrawingEvent -= handler;
        }

        public void Initialize(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
        }

        public void LoadContent(ContentManager manager)
        {
            Font = manager.Load<SpriteFont>("UI/MainFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch != null)
                DrawingEvent?.Invoke(spriteBatch);
        }
    }
}
