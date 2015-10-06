using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace TutorialRPG.MapEditor
{
    public class Layer
    {
        public class TileMap
        {
            [XmlElement("Row")]
            public List<string> Row { get; set; }

            public TileMap()
            {
                Row = new List<string>();
            }
        }

        public TileMap TilesMap { get; set; }
        public Image SpriteSheet { get; set; }

        [XmlIgnore]
        public List<Tile> Tiles { get; set; } 

        public Layer()
        {
            TilesMap = new TileMap();
            Tiles = new List<Tile>();
        }

        public void LoadContent(Vector2 tileDimensions)
        {
            SpriteSheet.LoadContent();
            Vector2 position = Vector2.Zero;
            
            foreach (string row in TilesMap.Row)
            {
                position.X = 0;
                foreach (var tile in row.Split(','))
                {
                    if (tile == string.Empty) continue;
                    Tile newTile = new Tile();

                    var values = tile.Split(':');
                    int x = int.Parse(values[0]);
                    int y = int.Parse(values[1]);

                    Rectangle sourceRect =
                        new Rectangle(x * (int)tileDimensions.X, y * (int)tileDimensions.Y,
                            (int)tileDimensions.X, (int)tileDimensions.Y);

                    newTile.LoadContent(position, sourceRect);
                        
                    Tiles.Add(newTile);

                    position.X += tileDimensions.X;
                }
                position.Y += tileDimensions.Y;
            }
        }

        public void UnloadContent()
        {
            SpriteSheet.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in Tiles)
            {
                SpriteSheet.position = tile.Position;
                SpriteSheet.SourceRect = tile.SourceRect;
                SpriteSheet.Draw(spriteBatch);
            }
        }
    }
}
