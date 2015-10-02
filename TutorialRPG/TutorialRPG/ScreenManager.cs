using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TutorialRPG
{
    public class ScreenManager
    {
        /// <summary>
        /// .Net Lazy object for singleton
        /// </summary>
        private static readonly Lazy<ScreenManager> Lazy =
            new Lazy<ScreenManager>(() =>
            {
                // Initializes XmlManager 
                var xml = new XmlManager<ScreenManager>();

                return xml.Load("Load/ScreenManager.xml");
            });

        /// <summary>
        /// Returns the singleton instance of the ScreenManager
        /// </summary>
        public static ScreenManager Instance
        {
            get { return Lazy.Value; }
        }

        /// <summary>
        /// Screen Dimensions
        /// </summary>
        public Vector2 Dimensions { get; set; }

        /// <summary>
        /// Content Manager instance to handle content in all game screens
        /// </summary>
        [XmlIgnore]
        public ContentManager Content { get; private set; }

        /// <summary>
        /// Image used for transitioning between screens
        /// </summary>
        public Image TransitionImage { get; set; }

        /// <summary>
        /// Current active GameScreen
        /// </summary>
        public GameScreen CurrentScreen { get; set; }

        /// <summary>
        /// Used to transition between screens
        /// Stores the screen to be transitioned to
        /// </summary>
        private GameScreen NextScreen { get; set; }

        /// <summary>
        /// Loads gamescreen through XML files
        /// </summary>
        private XmlManager<GameScreen> GameScreenLoader { get; set; }

        /// <summary>
        /// Indicates whether it currently transitioning between screens
        /// </summary>
        [XmlIgnore]
        public bool IsTransitioning { get; set; }

        /// <summary>
        /// Graphics device
        /// </summary>
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        /// Spritebatch
        /// </summary>
        [XmlIgnore]
        public SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// Initializes members of the ScreenManager class
        /// </summary>
        private ScreenManager()
        {
            Dimensions = new Vector2(640, 480);
            GameScreenLoader = new XmlManager<GameScreen>();
            CurrentScreen = new SplashScreen();
            GameScreenLoader.Type = CurrentScreen.Type;
            CurrentScreen = GameScreenLoader.Load("Load/SplashScreen.xml");
        }

        /// <summary>
        /// Loads the content ScreenManager and content it uses
        /// </summary>
        /// <param name="contentManager">Content Manager instance</param>
        public void LoadContent(ContentManager contentManager)
        {
            Content = new ContentManager(contentManager.ServiceProvider, "Content");
            CurrentScreen.LoadContent();
            TransitionImage.LoadContent();
        }

        /// <summary>
        /// Unloads the content used by ScreenManager
        /// </summary>
        public void UnloadContent()
        {
            CurrentScreen.UnloadContent();
            TransitionImage.UnloadContent();
        }

        /// <summary>
        /// Updates screens
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
            Transition(gameTime);
        }

        /// <summary>
        /// Draws the current screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
            if (IsTransitioning)
                TransitionImage.Draw(spriteBatch);
        }

        /// <summary>
        /// Changes the screen based on given screen name
        /// </summary>
        /// <param name="screenName">Screen to change to</param>
        public void ChangeScreen(string screenName)
        {
            if (!string.IsNullOrEmpty(screenName))
                NextScreen = (GameScreen)Activator.CreateInstance(Type.GetType("TutorialRPG." + screenName));
            TransitionImage.Scale = Dimensions;
            TransitionImage.IsActive = true;
            TransitionImage.FadeEffect.Increase = true;
            TransitionImage.Alpha = 0;
            IsTransitioning = true;
        }

        /// <summary>
        /// Perform the transition
        /// </summary>
        /// <param name="gameTime">Elapsed time of the game</param>
        private void Transition(GameTime gameTime)
        {
            if (!IsTransitioning) return;
            TransitionImage.Update(gameTime);
            if (TransitionImage.Alpha == 1f)
                StartTransition();
            else if (TransitionImage.Alpha == 0)
                EndTransition();
        }

        /// <summary>
        /// Stops transitioning
        /// </summary>
        private void EndTransition()
        {
            TransitionImage.IsActive = false;
            IsTransitioning = false;
        }

        /// <summary>
        /// Changes the screen to the next screen
        /// </summary>
        private void StartTransition()
        {
            CurrentScreen.UnloadContent();
            CurrentScreen = NextScreen;
            GameScreenLoader.Type = CurrentScreen.Type;
            if (File.Exists(CurrentScreen.XmlPath))
                CurrentScreen = GameScreenLoader.Load(CurrentScreen.XmlPath);
            CurrentScreen.LoadContent();
        }
    }
}
