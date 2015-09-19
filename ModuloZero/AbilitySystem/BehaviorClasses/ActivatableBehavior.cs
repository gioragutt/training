namespace AbilitySystem.BehaviorClasses
{
    public abstract class ActivatableBehavior : IBehavior
    {
        public abstract bool BehaviorImplentation(IUnit destinationPlayer);
        public abstract bool CanApplyBehaviorTo(IUnit unit);

        public void ApplyBehavior(IUnit unit)
        {
            if (!CanApplyBehaviorTo(unit)) return;
            BehaviorImplentation(unit);
        }
    }
}
