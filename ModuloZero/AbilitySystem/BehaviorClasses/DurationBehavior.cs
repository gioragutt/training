using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace AbilitySystem.BehaviorClasses
{
    public class DummyDurationBehavior : DurationBehavior
    {
        public int DummyInt { get; private set; }

        public DummyDurationBehavior(long durationOfBehavior, int dummyInt) 
            : base(durationOfBehavior)
        {
            DummyInt = dummyInt;
        }
        protected override void Activate(IUnit unit)
        {
            DummyInt += 5;
        }

        protected override void Deactivate(IUnit unit)
        {
            DummyInt -= 5;
        }
    }

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
            Timer.Stop();
            Deactivate(unit);
            IsActivated = false;
        }
    }
}
