namespace ModuloFramework.StatSystem
{
    public class VariableStat : Stat
    {
        public override float FinalValue
        {
            get { return BaseValue; }
        }

        public VariableStat(float baseValue)
            : base(baseValue) { }

        public override void AddRawBonus(RawBonus bonus)
        {
            BaseValue += bonus.BaseValue;
            if (BaseValue < 0)
                BaseValue = 0;
        }

        public override void RemoveRawBonus(RawBonus bonus)
        {
            BaseValue -= bonus.BaseValue;
            if (BaseValue < 0)
                BaseValue = 0;
        }

        public new class AsInt : VariableStat
        {
            public AsInt(int baseValue) : base(baseValue) { }

            public override float FinalValue
            {
                get { return (int)base.FinalValue; }
            }
        }
    }
}
