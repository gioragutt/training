namespace MonoGameFirst.BaseGameClasses.Item_System
{
    public static class AllItems
    {
        public static Item TestWeapon { get; private set; }
        public static Item TestConsumable { get; private set; }

        static AllItems()
        {
            TestWeapon = new Weapon("Test Weapon", 999, "Just a test weapon");
            TestConsumable = new Consumable("Test Consumable", 9999, "Just a test consumable");
        }
    }
}
