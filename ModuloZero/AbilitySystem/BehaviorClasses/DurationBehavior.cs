using System;
using System.Threading;
using System.Threading.Tasks;

namespace AbilitySystem.BehaviorClasses
{
    public abstract class DurationBehavior : LimitedTimeBehavior
    {
        protected IUnit AppliedUnit { get; private set; }
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
            AppliedUnit = unit;
            if (IsActivated) return;
            Thread trd = new Thread(ThreadMethod);
            trd.Start();
        }

        private void ThreadMethod()
        {
            IsActivated = true;
            Activate(AppliedUnit);
            Task.Delay(Duration).ContinueWith(_ => Deactivate(AppliedUnit));
            IsActivated = false;
        }
    }
}
