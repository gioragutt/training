using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameFirst.BaseGameClasses.Player_Classes.Stat_Classes
{
    public class StatAsInt : Stat
    {
        public StatAsInt(int baseValue) : base(baseValue) { }
        public new int FinalValue => (int)base.FinalValue;
    }
    public class Stat : BaseStat
    {
        private float finalValue;
        private List<RawBonus> RawBonuses { get; set; }
        private List<FinalBonus> FinalBonuses { get; set; }

        public float FinalValue => CalculateFinalValue();

        public Stat(float baseValue)
            : base(baseValue)
        {
            RawBonuses = new List<RawBonus>();
            FinalBonuses = new List<FinalBonus>();
        }

        #region Addition And Removal of Bonuses Methods

        public void AddRawBonus(RawBonus bonus)
        {
            RawBonuses.Add(bonus);
        }

        public void AddFinalBonus(FinalBonus bonus)
        {
            FinalBonuses.Add(bonus);
        }

        public void RemoveRawBonus(RawBonus bonus)
        {
            RawBonuses.Remove(bonus);
        }

        public void RemoveFinalBonus(FinalBonus bonus)
        {
            FinalBonuses.Remove(bonus);
        }

        #endregion

        #region Calculation Of Final Value Methods

        private float CalculateFinalValue()
        {
            finalValue = BaseValue;
            ApplyRawBonuses();
            ApplyFinalBonuses();
            return finalValue;
        }

        private void ApplyFinalBonuses()
        {
            float finalBonusValue = 0, finalBonusMultipier = 0;

            FinalBonuses.ForEach(bonus =>
            {
                finalBonusValue += bonus.BaseValue;
                finalBonusMultipier += BaseMultiplier;
            });

            finalValue += finalBonusValue;
            finalValue *= (1 + finalBonusMultipier);
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

        #endregion
    }
}
