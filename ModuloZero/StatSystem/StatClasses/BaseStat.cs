namespace StatSystem.StatClasses
{
    public class BaseStat
    {
        #region Object Methods

        protected static bool IsNull(BaseStat obj)
        {
            return ReferenceEquals(obj, null);
        }

        protected bool Equals(BaseStat other)
        {
            return BaseValue.Equals(other.BaseValue) && BaseMultiplier.Equals(other.BaseMultiplier);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((BaseStat)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (BaseValue.GetHashCode() * 397) ^ BaseMultiplier.GetHashCode();
            }
        }

        #endregion

        public float BaseValue { get; protected set; }
        public float BaseMultiplier { get; protected set; }

        public BaseStat(float baseValue, float baseMultiplier = 0)
        {
            BaseValue = baseValue;
            BaseMultiplier = baseMultiplier;
        }

        public static bool operator==(BaseStat lhs, BaseStat rhs)
        {
            if (IsNull(lhs))
                return false;
            return lhs.Equals(rhs);
        }

        public static bool operator!=(BaseStat lhs, BaseStat rhs)
        {
            return !(lhs == rhs);
        }
    }
}
