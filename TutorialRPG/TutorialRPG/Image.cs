using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TutorialRPG
{
    public class Image
    {
        private FadeEffect fadeEffect;

        /* Possible variables for xml */

        /// <summary>
        /// Alpha of the picture (0 = Transparent; 1 = Visible)
        /// </summary>
        public float Alpha { get; set; }

        /// <summary>
        /// Text to show
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Name of the font to load
        /// </summary>
        public string FontName { get; set; }

        /// <summary>
        /// Path to the image
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Position to put the photo in the game window
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Position of the text (relative to (0,0) of the picture)
        /// </summary>
        public Vector2 TextPosition { get; set; }

        /// <summary>
        /// Name of the color to draw the text in (default : white)
        /// </summary>
        public string TextColor { get; set; }

        /// <summary>
        /// Values to zoom in or out of the image
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// Rectangle to take the from the full image
        /// </summary>
        public Rectangle SourceRect { get; set; }

        /// <summary>
        /// A string of all effect names separated by spaces
        /// </summary>
        public string Effects { get; set; }

        /// <summary>
        /// Texture of the image
        /// </summary>
        private Texture2D Texture { get; set; }

        /// <summary>
        /// Origin for zoom-in and zoom-out
        /// </summary>
        private Vector2 Origin { get; set; }

        /// <summary>
        /// ContentManager to load textures and fonts
        /// </summary>
        private ContentManager Content { get; set; }

        /// <summary>
        /// Render target to make a texture out of the image and the text
        /// </summary>
        private RenderTarget2D RenderTarget { get; set; }

        /// <summary>
        /// Font of the text
        /// </summary>
        private SpriteFont Font { get; set; }

        /// <summary>
        /// A Dictionary of all effects 
        /// </summary>
        private Dictionary<string, ImageEffect> EffectList { get; set; }

        /// <summary>
        /// Gets and sets wether the image is active or not
        /// </summary>
        public bool IsActive { get; set; }

        public FadeEffect FadeEffect
        {
            get { return fadeEffect; }
            set { fadeEffect = value; }
        }

        public Image()
        {
            Path = Text = Effects = string.Empty;
            FontName = "ImageFont";
            Position = Vector2.Zero;
            TextColor = "White";
            Scale = Vector2.One;
            TextPosition = Vector2.Zero;
            Alpha = 1f;
            SourceRect = Rectangle.Empty;
            EffectList = new Dictionary<string, ImageEffect>();
        }

        public void LoadContent()
        {
            Content =
                new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            Font = Content.Load<SpriteFont>("Fonts/" + FontName);

            if (Path != string.Empty)
                Texture = Content.Load<Texture2D>(Path);

            Vector2 dimensions = new Vector2()
            {
                X = Texture?.Width ?? 0 + Font.MeasureString(Text).X,
                Y = Math.Max(Texture?.Height ?? 0, Font.MeasureString(Text).Y),
            };

            if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            Origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);

            RenderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, SourceRect.Width,
                SourceRect.Height);

            DrawImageTexture();

            Texture = RenderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            SetEffect(ref fadeEffect);

            if (Effects != string.Empty)
                foreach (var effect in Effects.Split(':'))
                    ActivateEffect(effect);
        }

        /// <summary>
        /// Parses a color name to a Microsoft.Xna.Framework.Color
        /// </summary>
        /// <param name="colorName">Name of the color</param>
        /// <returns>Microsoft.Xna.Framework.Color respectively to the given color name, or Color.White if name is invalid</returns>
        private static Color ColorFromName(string colorName)
        {
            var value = (Color?)typeof(Color).GetProperty(colorName)?.GetValue(null, null);
            return value ?? Color.White;
        }

        private void DrawImageTexture()
        {
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(RenderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if (Texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.DrawString(Font, Text, TextPosition, ColorFromName(TextColor));
            ScreenManager.Instance.SpriteBatch.End();
        }

        public void UnloadContent()
        {
            foreach (var effect in EffectList.Keys)
                DeactivateEffect(effect);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in EffectList.Values.Where(e => e.IsActive))
                effect.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRect, Color.White * Alpha, 0,
                Origin, Scale, SpriteEffects.None, 0);
        }

        private void ActivateEffect(ImageEffect effect, Image image)
        {
            effect.IsActive = true;
            var obj = this;
            effect.LoadContent(ref obj);
        }

        private void SetEffect<T>(ref T effect) where T : ImageEffect
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
                ActivateEffect(effect, this);

            EffectList.Add(effect.GetType().ToString().
                                  Replace(effect.GetType().Namespace + ".", ""), effect);
        }

        public void ActivateEffect(string effect)
        {
            if (EffectList.ContainsKey(effect))
                ActivateEffect(EffectList[effect], this);
        }

        public void DeactivateEffect(string effect)
        {
            if (!EffectList.ContainsKey(effect)) return;
            EffectList[effect].IsActive = false;
            EffectList[effect].UnloadContent();
        }
    }
}
