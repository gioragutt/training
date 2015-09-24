using ModuloFramework.AbilitySystem.Behaviors;

namespace ModuloFramework.AbilitySystem.Effects
{
    public class BehaviorApplyingEffect : IEffect
    {
        public IBehavior Behavior { get; }

        public BehaviorApplyingEffect(IBehavior behavior)
        {
            Behavior = behavior;
        }

        public void ActivateEffect(IUnit unit)
        {
            Behavior.ApplyBehavior(unit);
        }
    }
}
