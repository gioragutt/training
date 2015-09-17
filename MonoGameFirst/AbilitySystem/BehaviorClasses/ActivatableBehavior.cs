namespace AbilitySystem.BehaviorClasses
{
    public abstract class ActivatableBehavior : IBehavior
    {
        public abstract bool BehaviorImplentation(IUnit destinationPlayer);

        public bool ApplyBehavior(IUnit unit)
        {
            return BehaviorImplentation(unit);
        }
    }
}
