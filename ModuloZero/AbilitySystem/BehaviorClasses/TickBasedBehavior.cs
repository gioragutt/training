using System.Threading;

namespace AbilitySystem.BehaviorClasses
{
    public abstract class TickBasedBehavior : LimitedTimeBehavior
    {
        public long TickTime { get; }
        protected abstract void ApplyTick(IUnit unit);

        /// <summary>
        /// Initialize profile of the tick-based behavior
        /// </summary>
        /// <param name="durationOfBehavior">Total duration of the behavior</param>
        /// <param name="timeBetweenTicks">Time between each tick occurs</param>
        protected TickBasedBehavior(long durationOfBehavior, long timeBetweenTicks)
            : base(durationOfBehavior)
        {
            TickTime = timeBetweenTicks;
        }

        public override void ApplyBehavior(IUnit unit)
        {
            Thread trd = new Thread(() => ThreadMethod(unit));
            trd.Start();
        }

        private void ThreadMethod(IUnit unit)
        {
            Timer.Start();

            while (Timer.ElapsedMilliseconds < Duration)
            {
                if (Timer.Elapsed.TotalMilliseconds % TickTime == 0)
                    ApplyTick(unit);
            }

            Timer.Stop();
        }
    }
}
