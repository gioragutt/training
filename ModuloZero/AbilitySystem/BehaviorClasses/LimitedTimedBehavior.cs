using System.Diagnostics;

namespace AbilitySystem.BehaviorClasses
{
    public abstract class LimitedTimeBehavior : IBehavior
    {
        public Stopwatch Timer { get; }
        public abstract void ApplyBehavior(IUnit unit);
        public long Duration { get; }

        protected LimitedTimeBehavior(long durationOfBehavior)
        {
            Timer = new Stopwatch();
            Duration = durationOfBehavior;
        }
    }
}
