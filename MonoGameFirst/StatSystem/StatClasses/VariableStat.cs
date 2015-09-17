namespace StatSystem.StatClasses
{
    public class VariableStat : BaseStat
    {
        protected float valueOfStat;

        public float Value
        {
            get { return valueOfStat; }
            set { this.valueOfStat = value; }
        }

        public VariableStat(float baseValue)
            : base(baseValue)
        {
            Value = BaseValue;
        }

        #region AsInt

        public class AsInt : VariableStat
        {
            public new int Value
            {
                get { return (int)valueOfStat; }
                set { valueOfStat = (float)value; }
            }

            public AsInt(int baseValue) : base(baseValue) { }
        }

        #endregion
    }
}
