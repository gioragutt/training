using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TutorialRPG
{
    public class Player
    {
        private Vector2 velocity;
        public Image Image { get; set; }
        public float MoveSpeed { get; set; }

        public Player()
        {
            velocity = Vector2.Zero; 
        }

        public void LoadContent()
        {
            Image.LoadContent();
            Image.SpriteSheetEffect.SwitchFrame = 100 - ((int)MoveSpeed / (100 + (int)MoveSpeed)) * 100;
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Image.IsActive = true;
            if (InputManager.Instance.KeyDown(Keys.Down))
            {
                velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Image.SpriteSheetEffect.currentFrame.Y = 0;
            }
            else if (InputManager.Instance.KeyDown(Keys.Up))
            {
                velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Image.SpriteSheetEffect.currentFrame.Y = 3;
            }
            else
                velocity.Y = 0;

            if (InputManager.Instance.KeyDown(Keys.Right))
            {
                velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Image.SpriteSheetEffect.currentFrame.Y = 2;
            }
            else if (InputManager.Instance.KeyDown(Keys.Left))
            {
                velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Image.SpriteSheetEffect.currentFrame.Y = 1;
            }
            else
                velocity.X = 0;

            if (velocity.X == 0 && velocity.Y == 0)
                Image.IsActive = false;

            Image.position += velocity;
            Image.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
