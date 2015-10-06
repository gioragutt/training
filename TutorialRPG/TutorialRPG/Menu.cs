using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TutorialRPG
{
    public class Menu
    {
        private string id;
        public event EventHandler OnMenuChange;

        public string Axis { get; set; }
        public string Effects { get; set; }

        [XmlElement("Item")]
        public List<MenuItem> Items { get; set; }

        [XmlIgnore]
        public int ItemNumber { get; private set; }

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnMenuChange?.Invoke(this, null);
            }
        }

        public Menu()
        {
            ID = Effects = string.Empty;
            ItemNumber = 0;
            Axis = "Y";
            Items = new List<MenuItem>();
        }

        public void Transition(float alpha)
        {
            foreach (var item in Items)
            {
                item.Image.IsActive = true;
                item.Image.Alpha = alpha;
                item.Image.FadeEffect.Increase = alpha == 0;
            }
        }

        private void AlignMenuItems()
        {
            Vector2 dimensions = Items.Aggregate(Vector2.Zero, (current, item) => current + new Vector2(item.Image.SourceRect.Width, item.Image.SourceRect.Height));

            dimensions = new Vector2((ScreenManager.Instance.Dimensions.X - dimensions.X) / 2,
                (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2);

            foreach (var item in Items)
            {
                if (Axis == "X")
                    item.Image.position =
                        new Vector2(dimensions.X,
                            (ScreenManager.Instance.Dimensions.Y - item.Image.SourceRect.Height) / 2);
                else if (Axis == "Y")
                    item.Image.position =
                        new Vector2((ScreenManager.Instance.Dimensions.X - item.Image.SourceRect.Width) / 2,
                            dimensions.Y);

                dimensions += new Vector2(item.Image.SourceRect.Width,
                    item.Image.SourceRect.Height);
            }
        }

        public void LoadContent()
        {
            var effects = Effects.Split(':');
            foreach (MenuItem item in Items)
            {
                item.Image.LoadContent();
                foreach (var effect in effects)
                    item.Image.ActivateEffect(effect);
            }
            AlignMenuItems();
        }

        public void UnloadContent()
        {
            foreach (var item in Items)
            {
                item.Image.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateKeyboardInputForMenu();
            LimitedItemNumber();
            UpdateMenuItemImages(gameTime);
        }

        private void UpdateMenuItemImages(GameTime gameTime)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Image.IsActive = i == ItemNumber;
                Items[i].Image.Update(gameTime);
            }
        }

        private void LimitedItemNumber()
        {
            if (ItemNumber < 0)
                ItemNumber = 0;
            if (ItemNumber > Items.Count - 1)
                ItemNumber = Items.Count - 1;
        }

        private void UpdateKeyboardInputForMenu()
        {
            if (Axis == "X")
            {
                if (InputManager.Instance.KeyPressed(Keys.Right))
                    ItemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Left))
                    ItemNumber--;
            }
            else if (Axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down))
                    ItemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Up))
                    ItemNumber--;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in Items)
            {
                item.Image.Draw(spriteBatch);
            }
        }
    }
}
