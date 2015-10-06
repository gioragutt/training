using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TutorialRPG.Screens;

namespace TutorialRPG
{
    public class Image
    {
        private FadeEffect fadeEffect;

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
        public Vector2 position;

        private SpriteSheetEffect spriteSheetEffect;

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
        [XmlIgnore]
        public Texture2D Texture { get; set; }

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
        private Dictionary<string, ImageEffect> EffectList { get; }

        /// <summary>
        /// Gets and sets wether the image is active or not
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets the default font name, in case font name in xml is invalid
        /// </summary>
        private string DefaultFontName { get; }

        /// <summary>
        /// Fade effect instance
        /// </summary>
        public FadeEffect FadeEffect
        {
            get { return fadeEffect; }
            set { fadeEffect = value; }
        }

        /// <summary>
        /// SpriteSheet effect instance
        /// </summary>
        public SpriteSheetEffect SpriteSheetEffect
        {
            get { return spriteSheetEffect; }
            set { spriteSheetEffect = value; }
        }

        /// <summary>
        /// Initializes members the image class
        /// </summary>
        public Image()
        {
            Path = Text = Effects = string.Empty;
            DefaultFontName = FontName = "ImageFont";
            position = Vector2.Zero;
            TextColor = "White";
            Scale = Vector2.One;
            TextPosition = Vector2.Zero;
            Alpha = 1f;
            SourceRect = Rectangle.Empty;
            EffectList = new Dictionary<string, ImageEffect>();
        }

        /// <summary>
        /// Loads content from content manager, creates texture of image
        /// </summary>
        public void LoadContent()
        {
            Content =
                new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            try
            {
                Font = Content.Load<SpriteFont>("Fonts/" + FontName);
            }
            catch
            {
                Font = Content.Load<SpriteFont>("Fonts/" + DefaultFontName);
            }

            if (Path != string.Empty)
                Texture = Content.Load<Texture2D>(Path);

            Vector2 dimensions = new Vector2
            {
                X = Texture != null
                    ? Math.Max(Font.MeasureString(Text).X + TextPosition.X, Texture.Width)
                    : Font.MeasureString(Text).X + TextPosition.X,
                Y = Texture != null
                    ? Math.Max(Texture.Height, Font.MeasureString(Text).Y + TextPosition.Y)
                    : Font.MeasureString(Text).Y + TextPosition.Y
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
            SetEffect(ref spriteSheetEffect);

            if (Effects == string.Empty) return;
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

        /// <summary>
        /// Draws the background image and the text to the spritebatch and stores
        /// It in a render target
        /// </summary>
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

        /// <summary>
        /// Unloads all image content
        /// </summary>
        public void UnloadContent()
        {
            foreach (var effect in EffectList.Keys)
                DeactivateEffect(effect);
        }

        /// <summary>
        /// Updates the image
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void Update(GameTime gameTime)
        {
            foreach (var effect in EffectList.Values.Where(e => e.IsActive))
                effect.Update(gameTime);
        }

        /// <summary>
        /// Draws the image
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw the image on</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position + Origin, SourceRect, Color.White * Alpha, 0,
                Origin, Scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Stores the effect before transition
        /// </summary>
        public void StoreEffects()
        {
            Effects = string.Empty;
            foreach (var effect in EffectList.Where(effect => effect.Value.IsActive))
                Effects += effect.Key + ":";

            if (Effects != string.Empty)
                Effects.Remove(Effects.Length - 1);
        }

        /// <summary>
        /// Restores the effect after transition
        /// </summary>
        public void RestoreEffects()
        {
            foreach(var effect in EffectList)
                DeactivateEffect(effect.Key);

            foreach (string effect in Effects.Split(':'))
                ActivateEffect(effect);
        }

        /// <summary>
        /// Activates an image effect onto an image
        /// </summary>
        /// <param name="effect">Effect to activate</param>
        private void ActivateEffect(ImageEffect effect)
        {
            effect.IsActive = true;
            var obj = this;
            effect.LoadContent(ref obj);
        }

        /// <summary>
        /// Initializes an ImageEffect to the image
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="effect"></param>
        private void SetEffect<T>(ref T effect) where T : ImageEffect
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
                ActivateEffect(effect);

            EffectList.Add(effect.GetType().ToString().
                                  Replace(effect.GetType().Namespace + ".", ""), effect);
        }

        /// <summary>
        /// Activates an image effect
        /// </summary>
        /// <param name="effect">Name of the effect to activate</param>
        public void ActivateEffect(string effect)
        {
            if (EffectList.ContainsKey(effect))
                ActivateEffect(EffectList[effect]);
        }

        /// <summary>
        /// Deactivates an image effect
        /// </summary>
        /// <param name="effect">Name of effect to deactivate</param>
        public void DeactivateEffect(string effect)
        {
            if (!EffectList.ContainsKey(effect)) return;
            EffectList[effect].IsActive = false;
            EffectList[effect].UnloadContent();
        }
    }
}
