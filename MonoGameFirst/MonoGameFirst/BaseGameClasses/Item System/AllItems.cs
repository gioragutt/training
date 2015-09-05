using MonoGameFirst.BaseGameClasses.Player_Classes;

namespace MonoGameFirst.BaseGameClasses.Item_System
{
    public static class AllItems
    {
        public static Item TestWeapon { get; private set; }
        public static Item TestConsumable { get; private set; }

        static AllItems()
        {
            TestWeapon = 
                new Weapon(
                    name: "Test Weapon",
                    cost: 999,
                    description: "Just a test weapon",
                    playerStats: PlayerStats.Create(maxHealth: 10, movespeed: 2));

            TestConsumable =
                new Consumable(
                    name: "Test Consumable",
                    cost: 9999,
                    description: "Just a test consumable",
                    playerStats:PlayerStats.Create());
        }
    }
}
