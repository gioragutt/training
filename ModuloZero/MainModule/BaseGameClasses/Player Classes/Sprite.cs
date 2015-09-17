using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameFirst.BaseGameClasses.Player_Classes
{
    public class Sprite
    {
        #region Data Members

        private Vector2 position;

        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Texture2D Texture { get; set; }

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        #endregion

        #region Public Constructors

        public Sprite(Texture2D texture, int xPos, int yPos)
            : this(texture, new Vector2((float) xPos, (float) yPos))
        {
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.Position = position;
            this.Texture = texture;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        #endregion Public Methods
    }
}
