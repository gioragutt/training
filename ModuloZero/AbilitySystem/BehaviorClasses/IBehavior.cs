using MonoGameFirst.BaseGameClasses.Player_Classes;

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
        /// <param name="unit"></param>
        /// <returns>true if affection is successful; otherwise false</returns>
        bool ApplyBehavior(IUnit unit);
    }

    /*
    TODO:
    IBehavior -> ActivatableBehavior, TimedBehavior, TogglableAffect, PersistantEffect 
    */
}
