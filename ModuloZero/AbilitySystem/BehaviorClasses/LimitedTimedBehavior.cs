using System;

namespace AbilitySystem.BehaviorClasses
{
    public abstract class LimitedTimeBehavior : IBehavior
    {
        public abstract void ApplyBehavior(IUnit unit);
        public abstract bool CanApplyBehaviorTo(IUnit unit);

        public TimeSpan Duration { get; }

        protected LimitedTimeBehavior(TimeSpan durationOfBehavior)
        {
            Duration = durationOfBehavior;
        }
    }
}
