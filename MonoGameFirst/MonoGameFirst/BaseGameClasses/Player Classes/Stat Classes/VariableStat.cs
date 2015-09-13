namespace MonoGameFirst.BaseGameClasses.Player_Classes.Stat_Classes
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

    public class DependentVariableStat : VariableStat
    {
        protected readonly Stat dependency;

        public new float Value
        {
            get { return valueOfStat; }
            set { this.valueOfStat = SetMethod(value); }
        }

        public DependentVariableStat(float baseValue, Stat dependentStat)
            : base(baseValue)
        {
            Value = BaseValue;
            dependency = dependentStat;
        }

        #region Set Method

        private float SetMethod(float value)
        {
            return DefaultSetMethod(value);
        }

        private float DefaultSetMethod(float value)
        {
            /* ===================================================  *\
           ||   Must cast to object to have == null return true,     ||
           ||   Because it conflicts with the overloaded operator==  ||
            \* ===================================================  */
            if ((object)dependency == null)
                return valueOfStat;
            float maxVal = dependency.FinalValue;
            return value < 0 ? 0 : value > maxVal ? maxVal : value;
        }

        #endregion

        #region AsInt

        public new class AsInt : DependentVariableStat
        {
            public new int Value
            {
                get { return (int)valueOfStat; }
                set { valueOfStat = SetMethod(value); }
            }

            public AsInt(int baseValue, Stat dependentStat)
                : base(baseValue, dependentStat) { }
        }

        #endregion
    }
}
