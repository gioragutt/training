using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AbilitySystem.BehaviorClasses
{
    public class DummyPerTickBehavior : TickBasedTimedBehavior
    {
        public int DummyInt { get; private set; }

        public DummyPerTickBehavior(long durationOfBehavior, long timeBetweenTicks, int dummyInt)
            : base(durationOfBehavior, timeBetweenTicks)
        {
            DummyInt = dummyInt;
        }

        protected override void ApplyTick(IUnit unit)
        {
            DummyInt += 5;
        }
    }

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
