using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TutorialRPG.MapEditor;

namespace TutorialRPG.Screens
{
    public class GameplayScreen : GameScreen
    {
        public Player Player { get; set; }
        public Map Map { get; set; }

        public override void LoadContent()
        {
            base.LoadContent();
            LoadPlayer();
            LoadMap();
        }

        private void LoadMap()
        {
            var mapLoader = new XmlManager<Map>();
            Map = mapLoader.Load("Load/Gameplay/Map/Map1.xml");
            Map.LoadContent();
        }

        private void LoadPlayer()
        {
            var playerLoader = new XmlManager<Player>();
            Player = playerLoader.Load("Load/Gameplay/Player.xml");
            Player.LoadContent();
            ScreenManager.Instance.CenterImage(Player.Image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Player.UnloadContent();
            Map.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Player.Update(gameTime);
            Map.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Map.Draw(spriteBatch);
            Player.Draw(spriteBatch);
        }
    }
}
