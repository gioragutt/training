using System.Threading;

namespace AbilitySystem.BehaviorClasses
{
    public abstract class DurationBehavior : LimitedTimeBehavior
    {
        protected abstract void Activate(IUnit unit);
        protected abstract void Deactivate(IUnit unit);

        private bool IsActivated { get; set; }

        /// <summary>
        /// Initialize profile of behavior
        /// </summary>
        /// <param name="durationOfBehavior">Duration of the behavior in milliseconds</param>
        protected DurationBehavior(long durationOfBehavior)
            : base(durationOfBehavior)
        {
            IsActivated = false;
        }

        public override void ApplyBehavior(IUnit unit)
        {
            if (IsActivated) return;
            Thread trd = new Thread(() => ThreadMethod(unit));
            trd.Start();
            IsActivated = true;
        }

        private void ThreadMethod(IUnit unit)
        {
            Timer.Start();
            Activate(unit);
            while (Timer.Elapsed.TotalMilliseconds < Duration) { }
            Timer.Reset();
            Deactivate(unit);
            IsActivated = false;
        }
    }
}
