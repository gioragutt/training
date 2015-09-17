using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameFirst.BaseGameClasses.Interfaces;
using MonoGameFirst.BaseGameClasses.Player_Classes;
using MonoGameFirst.BaseGameClasses.Player_Classes.Stat_Classes;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MonoGameFirst.BaseGameClasses.Item_System
{
    public class Inventory : IDrawsOnUI, IKeyboardHandled
    {
        #region Properties

        public Player Player { get; private set; }
        private Dictionary<ItemType, Slot> Slots { get; set; }

        public PlayerStats Stats
        {
            get
            {
                PlayerStats stats = PlayerStats.Create();
                foreach (var slot in Slots)
                    if (slot.Value.Item != null)
                        stats += slot.Value.Item.Stats;
                return stats;
            }
        }

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
            if (Slots[item.Type].Item == item) return;
            Slots[item.Type].SetItem(item);
            Player.Stats += item.Stats;
        }

        public void UnequipItem(Item item)
        {
            if (!Slots.ContainsKey(item.Type) || Slots[item.Type].Item != item) return;
            Slots[item.Type].UnsetItem();
            Player.Stats -= item.Stats;
            Player.ValidateStats();
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
            const string FORMAT = "{0} {1,-12}{2,-17}{3,-8}{4,-15}";
            const string UNDERLINE = "------------  ---------------  ------  -----------------";
            const string TITLE_HEAD = "INDEX         NAME             COST    DESCRIPTION";
            string title = TITLE_HEAD + '\n' + UNDERLINE;
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
                    ? string.Format(format, slot.Value.Index, slot.Value.ItemType, slot.Value.Item.Name,
                        slot.Value.Item.Cost, slot.Value.Item.Description)
                    : slot.Value.Index.ToString() + " " + slot.Key.ToString();
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