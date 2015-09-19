using System;
using System.Threading;
using System.Threading.Tasks;

namespace AbilitySystem.BehaviorClasses
{
    public abstract class DurationBehavior : LimitedTimeBehavior
    {
        protected abstract void Activate(IUnit unit);
        protected abstract void Deactivate(IUnit unit);
        public override abstract bool CanApplyBehaviorTo(IUnit unit);

        private bool IsActivated { get; set; }

        /// <summary>
        /// Initialize profile of behavior
        /// </summary>
        /// <param name="durationOfBehavior">Duration of the behavior</param>
        protected DurationBehavior(TimeSpan durationOfBehavior)
            : base(durationOfBehavior)
        {
            IsActivated = false;
        }

        public override void ApplyBehavior(IUnit unit)
        {
            if (!CanApplyBehaviorTo(unit)) return;
            if (IsActivated) return;
            Thread trd = new Thread(_ => ThreadMethod(unit));
            trd.Start();
        }

        private void ThreadMethod(IUnit unit)
        {
            IsActivated = true;
            Activate(unit);
            Task.Delay(Duration).ContinueWith(_ => Deactivate(unit));
            IsActivated = false;
        }
    }
}
