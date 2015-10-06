using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TutorialRPG.Screens
{
    public class SplashScreen : GameScreen
    {
        public Image Image { get; set; }

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
            ScreenManager.Instance.CenterImage(Image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.Enter, Keys.Z))
                ScreenManager.Instance.ChangeScreen("TitleScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
