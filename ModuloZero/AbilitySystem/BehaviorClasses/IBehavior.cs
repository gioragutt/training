namespace AbilitySystem.BehaviorClasses
{
    /// <summary>
    /// Interface for any effect that affects a player
    /// </summary>
    public interface IBehavior
    {
        /// <summary>
        /// Affects a player in any way
        /// </summary>
        /// <param name="unit">unit to apply behavior to</param>
        void ApplyBehavior(IUnit unit);

        /// <summary>
        /// Returns whether a target is valid to apply the behavior to
        /// </summary>
        /// <param name="unit">unit to check for validity</param>
        /// <returns>true if target is valid to apply effect on; otherwise false</returns>
        bool CanApplyBehaviorTo(IUnit unit);
    }
}
