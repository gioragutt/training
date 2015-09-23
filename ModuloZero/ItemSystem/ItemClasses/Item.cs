using System;
using System.Diagnostics;
using System.Threading;
using AbilitySystem;
using AbilitySystem.AbilityClasses;
using AbilitySystem.BehaviorClasses;
using AbilitySystem.EffectClasses;
using ItemSystem.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UISystem;

namespace ItemSystem.ItemClasses
{
    /// <summary>
    /// Abstract class for any item
    /// </summary>
    public class Item
    {
        #region Properties

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

        #endregion

        #region Ctor

        public Item(int id, string name, int price, ItemType type, string baseDescription, Ability ability)
        {
            ID = id;
            Name = name;
            Price = price;
            Type = type;
            Ability = ability;
            Description = string.Format("{0}: {1}\n{2}: {3}", Name, baseDescription, Ability.Name, Ability.Description);
        }

        #endregion

        #region Methods

        public void Activate(IUnit unit)
        {
            if (Ability.IsActivatable)
                Ability.ActivateAbility(unit);
        }

        #endregion
    }

    public class TestItem
    {
        public ItemSystem.ItemClasses.Item Item { get; }

        private class ItemBehavior : ActivatableBehavior
        {
            private bool isDrawn;

            public ItemBehavior()
            {
                UI.SubscribeToUIDraw(PrintUi);
                isDrawn = false;
            }

            protected override void BehaviorImplementation(IUnit destinationPlayer)
            {
                Debug.Print("Behavior test print");
                isDrawn = !isDrawn;
            }

            public override bool CanApplyBehaviorTo(IUnit unit)
            {
                return true;
            }

            private void PrintUi(SpriteBatch spriteBatch)
            {
                if (!isDrawn) return;
                spriteBatch.DrawString(UI.Font, string.Format("Test"), new Vector2(20, 50),
                    Color.Black);
            }
        }

        public TestItem()
        {
            Item = new Item(id: 0,
                name: "Test Item",
                price: 30,
                type: ItemType.Weapon,
                baseDescription: "Just a test item",
                ability: new Ability(effect: new BehaviorApplyingEffect(new ItemBehavior()),
                    isActivatable: true,
                    name: "Test ability",
                    isUnique: false,
                    description: "Just a test ability",
                    cooldown: TimeSpan.FromSeconds(3)));
        }
    }
}
