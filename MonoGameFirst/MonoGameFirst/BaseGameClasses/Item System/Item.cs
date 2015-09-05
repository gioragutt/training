using System;
using System.Collections.Generic;
using System.Linq;
using MonoGameFirst.BaseGameClasses.Player_Classes;

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
        public override PlayerStats Stats { get; protected set; }
        public Weapon(string name, int cost, string description, PlayerStats playerStats) 
            : base(name, cost, description, playerStats) { }
    }

    public class Consumable : Item
    {
        public override string Name { get; protected set; }
        public override int Cost { get; protected set; }
        public override string Description { get; protected set; }
        public override PlayerStats Stats { get; protected set; }
        public Consumable(string name, int cost, string description, PlayerStats playerStats) 
            : base(name, cost, description, playerStats) { }
    }

    public abstract class Item
    {
        public abstract string Name { get; protected set; }
        public abstract int Cost { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract PlayerStats Stats { get; protected set; }

        protected Item(string name, int cost, string description, PlayerStats playerStats)
        {
            Name = name;
            Cost = cost;
            Description = description;
            Stats = playerStats;
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
