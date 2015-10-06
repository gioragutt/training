using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TutorialRPG.MapEditor
{
    public class Tile
    {
        public Vector2 Position { get; set; }
        public Rectangle SourceRect { get; set; }

        public void LoadContent(Vector2 position, Rectangle sourceRect)
        {
            Position = position;
            SourceRect = sourceRect;
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
