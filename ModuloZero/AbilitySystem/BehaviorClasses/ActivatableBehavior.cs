namespace AbilitySystem.BehaviorClasses
{
    public abstract class ActivatableBehavior : IBehavior
    {
        public abstract bool BehaviorImplementation(IUnit destinationPlayer);
        public abstract bool CanApplyBehaviorTo(IUnit unit);

        public void ApplyBehavior(IUnit unit)
        {
            if (!CanApplyBehaviorTo(unit)) return;
            BehaviorImplementation(unit);
        }
    }
}
