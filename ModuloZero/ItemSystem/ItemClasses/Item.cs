namespace ItemSystem.ItemClasses
{
    /// <summary>
    /// Abstract class for any item
    /// </summary>
    public abstract class Item
    {
        public abstract string Name { get; }
        public abstract int Price { get; }
        public abstract Enums.ItemType Type { get; }
    }
}
