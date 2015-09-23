using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.BehaviorClasses
{
    public abstract class TogglableBehavior : IBehavior
    {
        protected bool IsToggledOn { get; set; }

        public abstract bool CanApplyBehaviorTo(IUnit unit);
        protected abstract void ToggleOn();
        protected abstract void ToggleOff();

        protected TogglableBehavior()
        {
            IsToggledOn = false;
        }

        private void ToggleBehavior()
        {
            if (IsToggledOn)
                ToggleOff();
            else
                ToggleOn();
            IsToggledOn = !IsToggledOn;
        }

        public void ApplyBehavior(IUnit unit)
        {
            ToggleBehavior();
        }
    }
}
