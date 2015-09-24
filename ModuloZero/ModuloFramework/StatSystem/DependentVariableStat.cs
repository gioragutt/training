using System;

namespace ModuloFramework.StatSystem
{
    public class DependentVariableStat : VariableStat
    {
        protected readonly Stat dependency;
        private readonly Func<float, float, float> dependencyMethod;

        public DependentVariableStat(float baseValue, Stat dependentStat,
                                     Func<float, float, float> dependencyAppliance = null)
            : base(baseValue)
        {
            dependency = dependentStat;
            dependencyMethod = dependencyAppliance;
        }

        public override void AddRawBonus(RawBonus bonus)
        {
            base.AddRawBonus(bonus);
            ApplyDependency();
        }

        public override void RemoveRawBonus(RawBonus bonus)
        {
            base.RemoveRawBonus(bonus);
            ApplyDependency();
        }

        public void ApplyDependency()
        {
            float valueOfDepedency = dependency.FinalValue;
            if (dependencyMethod != null)
                BaseValue = dependencyMethod(BaseValue, valueOfDepedency);
            else
                BaseValue = BaseValue < 0 ? 0 : BaseValue > valueOfDepedency ? valueOfDepedency : BaseValue;
        }
    }
}
