using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TutorialRPG
{
    public class MenuManager
    {
        public Menu Menu { get; set; }
        private bool IsTransitioning { get; set; }

        public MenuManager()
        {
            Menu = new Menu();
            Menu.OnMenuChange += Menu_OnMenuChange;
        }

        private void Transition(GameTime gameTime)
        {
            if (!IsTransitioning) return;
            for (int i = 0; i < Menu.Items.Count; i++)
            {
                MenuItem item = Menu.Items[i];
                item.Image.Update(gameTime);
                float firstItemAlpha = Menu.Items[0].Image.Alpha;
                float lastItemAlpha = Menu.Items[Menu.Items.Count - 1].Image.Alpha;
                if (firstItemAlpha == 0 && lastItemAlpha == 0)
                    Menu.ID = Menu.Items[Menu.ItemNumber].LinkID;
                else if (firstItemAlpha == 1f && lastItemAlpha == 1)
                {
                    IsTransitioning = false;
                    foreach (var menuItem in Menu.Items)
                        menuItem.Image.RestoreEffects();
                }
            }
        }

        private void Menu_OnMenuChange(object sender, EventArgs e)
        {
            var menuLoader = new XmlManager<Menu>();
            Menu.UnloadContent();
            Menu = menuLoader.Load(Menu.ID);
            Menu.LoadContent();
            Menu.OnMenuChange += Menu_OnMenuChange;
            Menu.Transition(0);

            foreach (var item in Menu.Items)
            {
                item.Image.StoreEffects();
                item.Image.ActivateEffect("FadeEffect");
            }
        }

        public void LoadContent(string menuPath)
        {
            if (menuPath != string.Empty)
                Menu.ID = menuPath;
        }

        public void UnloadContent()
        {
            Menu.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (!IsTransitioning)
                Menu.Update(gameTime);
            if (InputManager.Instance.KeyPressed(Keys.Enter) && !IsTransitioning)
            {
                string linkType = Menu.Items[Menu.ItemNumber].LinkType;
                if (linkType == "Screen")
                    ScreenManager.Instance.ChangeScreen(Menu.Items[Menu.ItemNumber].LinkID);
                else if (linkType == "Menu")
                {
                    IsTransitioning = true;
                    Menu.Transition(1f);
                    foreach (var item in Menu.Items)
                    {
                        item.Image.StoreEffects();
                        item.Image.ActivateEffect("FadeEffect");
                    }
                }
            }
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Menu.Draw(spriteBatch);
        }
    }
}
