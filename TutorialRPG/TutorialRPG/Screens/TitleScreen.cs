using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TutorialRPG.Screens
{
    public class TitleScreen : GameScreen
    {
        private MenuManager MenuManager { get; set; }

        public TitleScreen()
        {
            MenuManager = new MenuManager();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            MenuManager.LoadContent("Load/Menu/TitleMenu.xml");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            MenuManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MenuManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            MenuManager.Draw(spriteBatch);
        }
    }
}
