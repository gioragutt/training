namespace AbilitySystem.BehaviorClasses
{
    public abstract class TickBasedTimedBehavior : LimitedTimeBehavior
    {
        public long TickTime { get; }
        protected abstract void ApplyTick(IUnit unit);

        /// <summary>
        /// Initialize profile of the tick-based behavior
        /// </summary>
        /// <param name="durationOfBehavior">Total duration of the behavior</param>
        /// <param name="timeBetweenTicks">Time between each tick occurs</param>
        protected TickBasedTimedBehavior(long durationOfBehavior, long timeBetweenTicks)
            : base(durationOfBehavior)
        {
            TickTime = timeBetweenTicks;
        }

        public override bool ApplyBehavior(IUnit unit)
        {
            Timer.Start();

            while (Timer.ElapsedMilliseconds < Duration)
                if (Timer.ElapsedMilliseconds % TickTime == 0)
                    ApplyTick(unit);

            Timer.Stop();
            return true;
        }
    }
}
