using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameFirst
{
    public class Sprite
    {
        #region Data Members

        private Vector2 m_position;

        #endregion

        #region Properties

        public Vector2 Position
        {
            get
            {
                return m_position;
            }
            set
            {
                m_position = value;
            }
        }

        public float X
        {
            get
            {
                return m_position.X;
            }
            set
            {
                m_position.X = value;
            }
        }

        public float Y
        {
            get
            {
                return m_position.Y;
            }
            set
            {
                m_position.Y = value;
            }
        }

        public Texture2D Texture
        {
            get; set;
        }

        public int Width
        {
            get
            {
                return Texture.Width;
            }
        }

        public int Height
        {
            get
            {
                return Texture.Height;
            }
        }

        #endregion

        #region Public Constructors

        public Sprite(Texture2D texture, int x_pos, int y_pos) 
            : this(texture, new Vector2((float)x_pos, (float)y_pos))
        { }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.Position = position;
            this.Texture = texture;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        #endregion Public Methods
    }
}
