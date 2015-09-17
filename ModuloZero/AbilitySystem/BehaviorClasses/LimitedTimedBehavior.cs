using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AbilitySystem.BehaviorClasses
{
    public abstract class LimitedTimeBehavior : IBehavior
    {
        public Stopwatch Timer { get; }
        public abstract bool ApplyBehavior(IUnit unit);
        public long Duration { get; }

        protected LimitedTimeBehavior(long durationOfBehavior)
        {
            Timer = new Stopwatch();
            Duration = durationOfBehavior;
        }
    }
}
