namespace AbilitySystem.BehaviorClasses
{
    public abstract class ActivatableBehavior : IBehavior
    {
        public abstract bool BehaviorImplentation(IUnit destinationPlayer);

        public void ApplyBehavior(IUnit unit)
        {
            BehaviorImplentation(unit);
        }
    }
}
