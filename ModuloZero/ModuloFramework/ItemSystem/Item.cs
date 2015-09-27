using System;
using System.Diagnostics;
using ItemSystem.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModuloFramework.AbilitySystem;
using ModuloFramework.AbilitySystem.Abilities;
using ModuloFramework.AbilitySystem.Behaviors;
using ModuloFramework.AbilitySystem.Effects;
using ModuloFramework.UISystem;

namespace ModuloFramework.ItemSystem
{
    public class Item
    {
        /// <summary>
        /// The ID of the item, shared between server and client
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Name of the item
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Price of the item
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Prototype: Type of the item
        /// RemoveIf: I decide not to do type-based slots
        /// </summary>
        public ItemType Type { get; }

        /// <summary>
        /// Base description of the item
        /// Additional info will be added based on stats and effects
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The ability of the item, can be a stat modifier, an activated ability or any other variation
        /// </summary>
        public Ability Ability { get; }

        private Item(int id, string name, int price, ItemType type, string baseDescription, Ability ability)
        {
            ID = id;
            Name = name;
            Price = price;
            Type = type;
            Ability = ability;
            Description = string.Format("{0}\n{1}: {2}", baseDescription, Ability.Name, Ability.Description);
        }

        public static Item Create(int id, string name, int price, ItemType type, string description, Ability ability)
        {
            return new Item(id, name, price, type, description, ability);
        }

        public void Activate(IUnit unit)
        {
            if (Ability != null && Ability.IsActivatable)
                Ability.ActivateAbility(unit);
        }
    }

    public class TestItem
    {
        public static Item Item1 { get; }
        public static Item Item2 { get; }

        /// <summary>
        /// Example of a behavior that can take a parameter with which it will apply the behavior
        /// </summary>
        private class DealDamageBehavior : ActivatableBehavior
        {
            private int Damage { get; set; }

            protected override void BehaviorImplementation(IUnit destinationPlayer)
            {
                /*
                
                Deal the Damage variable to the Unit(when IUnit will be implemented)

                */
            }

            public override bool CanApplyBehaviorTo(IUnit unit)
            {
                /*
                
                Check if `unit` is a valid unit to apply this behavior on

                */

                return true;
            }

            public DealDamageBehavior(int damageToDeal)
            {
                Damage = damageToDeal;
            }
        }

        /// <summary>
        /// Example of a behavior that can happen on it's own (has some debug print on Debug output and UI)
        /// </summary>
        private class ItemBehavior : ActivatableBehavior, IDrawsOnUI
        {
            private bool isDrawn;

            public ItemBehavior(IDrawingEngine drawingEngine)
            {
                InitializeDrawingEngine(drawingEngine);
                DrawingEngine.SubscribeToUIDraw(PrintUi);
                isDrawn = false;
            }

            protected override void BehaviorImplementation(IUnit destinationPlayer)
            {
                Debug.Print("Behavior test print");
                isDrawn = !isDrawn;
            }

            private void PrintUi(SpriteBatch spriteBatch)
            {
                if (!isDrawn) return;
                spriteBatch.DrawString(DrawingEngine.DefaultFont, string.Format("Test"), new Vector2(20, 50),
                    Color.Black);
            }

            public override bool CanApplyBehaviorTo(IUnit unit)
            {
                return true;
            }

            public IDrawingEngine DrawingEngine { get; set; }

            public void InitializeDrawingEngine(IDrawingEngine drawingEngine)
            {
                DrawingEngine = drawingEngine;
            }
        }

        static TestItem()
        {
            Item1 = Item.Create
                (
                    id: 0,
                    name: "Test Item",
                    price: 30,
                    type: ItemType.Weapon,
                    description: "Just a test item",
                    ability: Ability.CreateActivatable
                        (
                            effect: new BehaviorApplyingEffect(new ItemBehavior(UI.Instance)),
                            name: "Test ability",
                            isUnique: false,
                            description: "Just a test ability",
                            cooldown: TimeSpan.FromSeconds(3)
                        )
                );

            Item2 = Item.Create
                (
                    id: 1,
                    name: "Second Test Item",
                    price: 50,
                    type: ItemType.Armor,
                    description: "Just another test item",
                    ability: Ability.CreateActivatable
                        (
                            effect: new BehaviorApplyingEffect(new DealDamageBehavior(5)),
                            name: "Test ability that deals 5 damage",
                            isUnique: false,
                            description: "Just a test ability that deals 5 damage",
                            cooldown: TimeSpan.FromSeconds(3)
                        )
                );
        }
    }
}
