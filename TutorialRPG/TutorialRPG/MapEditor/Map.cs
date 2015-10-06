using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace TutorialRPG.MapEditor
{
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layer { get; set; }
        public Vector2 TileDimensions { get; set; }

        public Map()
        {
            Layer = new List<Layer>();
            TileDimensions = Vector2.Zero;
        }

        public void LoadContent()
        {
            Layer.ForEach(x => x.LoadContent(TileDimensions));
        }

        public void UnloadContent()
        {
            Layer.ForEach(x => x.UnloadContent());
        }

        public void Update(GameTime gameTime)
        {
            Layer.ForEach(x => x.Update(gameTime));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Layer.ForEach(x => x.Draw(spriteBatch));
        }
    }
}
