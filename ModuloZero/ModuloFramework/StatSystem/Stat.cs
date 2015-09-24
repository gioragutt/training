using System.Collections.Generic;

namespace ModuloFramework.StatSystem
{
    public class Stat : BaseStat
    {
        private bool WasChanged { get; set; }
        private float finalValue;
        private List<RawBonus> RawBonuses { get; set; }
        public virtual float FinalValue => CalculateFinalValue();

        public Stat(float baseValue)
            : base(baseValue)
        {
            RawBonuses = new List<RawBonus>();
            finalValue = baseValue;
            WasChanged = false;
        }

        public virtual void AddRawBonus(RawBonus bonus)
        {
            RawBonuses.Add(bonus);
            WasChanged = true;
        }

        public virtual void RemoveRawBonus(RawBonus bonus)
        {
            RawBonuses.Remove(bonus);
            WasChanged = true;
        }

        private float CalculateFinalValue()
        {
            if (!WasChanged) return finalValue;
            finalValue = BaseValue;
            ApplyRawBonuses();
            WasChanged = false;
            return finalValue;
        }

        private void ApplyRawBonuses()
        {
            float rawBonusValue = 0, rawBonusMultipier = 0;

            RawBonuses.ForEach(bonus =>
            {
                rawBonusValue += bonus.BaseValue;
                rawBonusMultipier += BaseMultiplier;
            });

            finalValue += rawBonusValue;
            finalValue *= (1 + rawBonusMultipier);
        }

        public class AsInt : Stat
        {
            public AsInt(int baseValue) : base(baseValue) { }
            public override float FinalValue => (int)base.FinalValue;
        }
    }
}
