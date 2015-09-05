using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameFirst.BaseGameClasses.Item_System
{
    public enum ItemType
    {
        Weapon,
        Consumable
    }

    public class Weapon : Item
    {
        public override string Name { get; protected set; }
        public override int Cost { get; protected set; }
        public override string Description { get; protected set; }
        public Weapon(string name, int cost, string description) : base(name, cost, description) { }
    }

    public class Consumable : Item
    {
        public override string Name { get; protected set; }
        public override int Cost { get; protected set; }
        public override string Description { get; protected set; }
        public Consumable(string name, int cost, string description) : base(name, cost, description) { }
    }

    public abstract class Item
    {
        public abstract string Name { get; protected set; }
        public abstract int Cost { get; protected set; }
        public abstract string Description { get; protected set; }

        protected Item(string name, int cost, string description)
        {
            Name = name;
            Cost = cost;
            Description = description;
        }

        public ItemType Type
        {
            get { return GetTypeOfItem(); }
        }
        private ItemType GetTypeOfItem()
        {
            if (this is Weapon)
                return ItemType.Weapon;
            else
                return ItemType.Consumable;
        }
    }
}
