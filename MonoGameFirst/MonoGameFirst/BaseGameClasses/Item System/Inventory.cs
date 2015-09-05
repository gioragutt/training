using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameFirst.BaseGameClasses.Interfaces;
using MonoGameFirst.BaseGameClasses.Player_Classes;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MonoGameFirst.BaseGameClasses.Item_System
{
    public class Inventory : IDrawsOnUI, IKeyboardHandled
    {
        #region Properties

        public Player Player { get; private set; }
        private Dictionary<ItemType, Slot> Slots { get; set; }

        #endregion

        #region Constructor

        public Inventory(Player sourcePlayer)
        {
            Player = sourcePlayer;
            Slots = new Dictionary<ItemType, Slot>();
            ShouldDraw = false;
        }

        #endregion

        #region Item Methods

        public void EquipItem(Item item)
        {
            Slots[item.Type].SetItem(item);
        }

        public void UnequipItem(Item item)
        {
            if (Slots.ContainsKey(item.Type) && Slots[item.Type].Item == item)
                Slots[item.Type].UnsetItem();
        }

        #endregion

        #region XNA Methods

        public void Initialize()
        {
            int index = 1;
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                Slots[type] = new Slot(type, index++);
            }
            SubscribeToKeyboardHandler();
            UI.SubscribeToUIDraw(UIDraw);
        }

        public void Update()
        {
            if (KeyboardHandler.IsKeyPressedOnce(Keys.Tab))
                ToggleDraw();
        }

        #endregion

        #region IDrawsOnUI Methods

        public bool ShouldDraw { get; set; }

        public void UIDraw(SpriteBatch spriteBatch)
        {
            if (!ShouldDraw)
                return;

            #region Variables and Constants

            const int WIDTH = 20;
            const string FORMAT = "{0,-8}{1,-18}{2,-11}{3,-8}{4,-15}";
            const string UNDERLINE = "------- ----------------- ---------- ------- ---------------";
            string titleHead = string.Format(FORMAT, "Index", "Name", "Type", "Cost", "Description");
            string title = titleHead + '\n' + UNDERLINE;
            int height = 40;

            #endregion

            spriteBatch.DrawString(UI.Font, title, new Vector2(WIDTH, height), Color.Black);
            height += (int)UI.Font.MeasureString(title).Y + 5;
            PrintItemListInUI(spriteBatch, FORMAT, height, WIDTH);
        }

        private void PrintItemListInUI(SpriteBatch spriteBatch, string format, int height, int width)
        {
            foreach (var slot in Slots)
            {
                var name = slot.Value.Item != null
                    ? string.Format(format, slot.Value.Index, slot.Value.Item.Name, slot.Value.ItemType,
                        slot.Value.Item.Cost, slot.Value.Item.Description)
                    : slot.Value.Index.ToString();
                Vector2 sizeOfMessage = UI.Font.MeasureString(name);
                spriteBatch.DrawString(UI.Font, name, new Vector2(width, height), Color.Black);
                height += (int)sizeOfMessage.Y + 5;
            }
        }

        public void ToggleDraw()
        {
            ShouldDraw = !ShouldDraw;
        }

        #endregion

        #region IKeyboardHandled Methods

        public void SubscribeToKeyboardHandler()
        {
            KeyboardHandler.SubscribeToKeyPressEvent(Keys.D1, () => { EquipItem(AllItems.TestWeapon); });
            KeyboardHandler.SubscribeToKeyPressEvent(Keys.D2, () => { EquipItem(AllItems.TestConsumable); });
            KeyboardHandler.SubscribeToKeyPressEvent(Keys.Z, () => { UnequipItem(AllItems.TestWeapon); });
            KeyboardHandler.SubscribeToKeyPressEvent(Keys.X, () => { UnequipItem(AllItems.TestConsumable); });
        }

        public void UnubscribeFromKeyboardHandler() { }

        public void ToggleSubscriptionToKeyboardHandler(bool toSubscribe = true) { }

        #endregion
    }
}