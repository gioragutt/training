using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameFirst.BaseGameClasses.Item_System
{
    public class Slot
    {
        public int Index { get; private set; }
        public Item Item { get; private set; }
        public ItemType ItemType { get; private set; }

        public Slot(ItemType type, int index)
        {
            ItemType = type;
            Index = index;
        }

        public void SetItem(Item itemToSet)
        {
            if (itemToSet.Type == ItemType)
                Item = itemToSet;
        }

        public void UnsetItem()
        {
            Item = null;
        }
    }
}
