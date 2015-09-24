namespace ModuloFramework.AbilitySystem.Behaviors
{
    public abstract class ActivatableBehavior : IBehavior
    {
        protected abstract void BehaviorImplementation(IUnit destinationPlayer);
        public abstract bool CanApplyBehaviorTo(IUnit unit);

        public void ApplyBehavior(IUnit unit)
        {
            if (!CanApplyBehaviorTo(unit)) return;
            BehaviorImplementation(unit);
        }
    }
}
