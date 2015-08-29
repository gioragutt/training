using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameFirst
{
    public class Sprite
    {
        #region Data Members

        public Rectangle Rectangle;

        #endregion Data Members

        #region Properties

        public Texture2D Texture
        {
            get;
            set;
        }

        #endregion

        #region Public Constructors

        public Sprite(Rectangle rectangle, Texture2D texture)
        {
            this.Rectangle = rectangle;
            this.Texture = texture;
        }

        public Sprite(Texture2D texture, int x_pos, int y_pos)
        {
            this.Texture = texture;
            this.Rectangle = new Rectangle(x_pos, y_pos, this.Texture.Width, this.Texture.Height);
        }

        #endregion Public Constructors

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);
        }

        #endregion Public Methods
    }
}
