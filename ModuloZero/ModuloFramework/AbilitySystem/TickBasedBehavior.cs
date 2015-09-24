using System;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloFramework.AbilitySystem.Behaviors
{
    public abstract class TickBasedBehavior : LimitedTimeBehavior
    {
        protected abstract void ApplyTick(IUnit unit);
        public abstract override bool CanApplyBehaviorTo(IUnit unit);

        public TimeSpan TickTime { get; }

        /// <summary>
        /// Initialize profile of the tick-based behavior
        /// </summary>
        /// <param name="durationOfBehavior">Total duration of the behavior</param>
        /// <param name="timeBetweenTicks">Time between each tick occurs</param>
        protected TickBasedBehavior(TimeSpan durationOfBehavior, TimeSpan timeBetweenTicks)
            : base(durationOfBehavior)
        {
            TickTime = timeBetweenTicks;
        }

        public override void ApplyBehavior(IUnit unit)
        {
            if (!CanApplyBehaviorTo(unit)) return;
            Thread trd = new Thread(() => ThreadMethod(unit));
            trd.Start();
        }

        private void ThreadMethod(IUnit unit)
        {
            TimeSpan totalTime = TimeSpan.Zero;
            while (totalTime.CompareTo(Duration) < 0)
            {
                Task.Delay(TickTime).ContinueWith(_ => ApplyTick(unit));
                totalTime += TickTime;
            }
        }
    }
}
