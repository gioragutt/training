using System;
using System.Collections.Generic;

namespace AbilitySystem.EffectClasses
{
    public class EffectSet : IEffect
    {
        public List<IEffect> Effects { get; }

        public EffectSet(List<IEffect> effects, Comparison<IEffect> comparer = null)
        {
            Effects = effects;
            if(comparer != null)
                Effects.Sort(comparer);
        }

        public void ActivateEffect(IUnit unit)
        {
            Effects.ForEach(e => e.ActivateEffect(unit));
        }
    }
}
