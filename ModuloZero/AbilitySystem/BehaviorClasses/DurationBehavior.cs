using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AbilitySystem.BehaviorClasses
{
    public abstract class DurationBehavior : LimitedTimeBehavior
    {
        protected abstract void Activate(IUnit unit);
        protected abstract void Deactivate(IUnit unit);

        /// <summary>
        /// Initialize profile of behavior
        /// </summary>
        /// <param name="durationOfBehavior">Duration of the behavior in milliseconds</param>
        protected DurationBehavior(long durationOfBehavior)
            : base(durationOfBehavior) { }

        public override bool ApplyBehavior(IUnit unit)
        {
            Timer.Start();
            Activate(unit);

            #region Kill Time Until Duration Of Effect Ends

            while (Timer.ElapsedMilliseconds < Duration) { }

            #endregion

            Timer.Stop();
            Deactivate(unit);
            return true;
        }
    }
}
