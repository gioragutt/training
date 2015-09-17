namespace StatSystem.StatClasses
{
    public class BooleanStat : BaseStat
    {
        public bool Value
        { get; set; }

        public BooleanStat(bool baseValue) : base(0)
        {
            Value = baseValue;
        }
    }
}
