using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TutorialRPG
{
    public abstract class GameScreen
    {
        protected ContentManager Content { get; set; }

        [XmlIgnore]
        public Type Type
        {
            get { return GetType(); }
        }

        public string XmlPath { get; set; }

        protected GameScreen()
        {
            XmlPath = "Load/" + Type.ToString().Replace(Type.Namespace + ".", "") + ".xml";
        }

        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent() => Content.Unload();

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
