using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AbilitySystem.EffectClasses;

namespace AbilitySystem.AbilityClasses
{
    public class Ability
    {
        #region Properties

        /// <summary>
        /// Effect of the ability, can be an effect set or any singular effect 
        /// </summary>
        public IEffect Effect { get; set; }

        /// <summary>
        /// Indicates whether the ability is active or passive
        /// </summary>
        public bool IsActivatable { get; set; }

        /// <summary>
        /// The cooldown of the ability if it is active
        /// </summary>
        public TimeSpan? Cooldown { get; set; }

        /// <summary>
        /// CooldownTimer for the cooldown of the ability, only created if the ability is active
        /// </summary>
        public Stopwatch CooldownTimer { get; set; }

        /// <summary>
        /// Name of the ability. Some abilities may share the name if they are part of a unique ability set
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicates wether the ability is unique
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// The description of the ability
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Thread for performing the ability
        /// </summary>
        private Thread AbilityThread { get; set; }

        #endregion

        #region Ctor

        public static Ability CreateActivatable(IEffect effect, string name, bool isUnique, string description,
                                         TimeSpan cooldown)
        {
            return new Ability(effect, true, name, isUnique, description, cooldown);
        }

        public static Ability CreateNotActivatable(IEffect effect, string name, bool isUnique, string description)
        {
            return new Ability(effect, false, name, isUnique, description);
        }

        private Ability(IEffect effect, bool isActivatable, string name, bool isUnique, string description,
                       TimeSpan? cooldown = null)
        {
            Effect = effect;
            IsActivatable = isActivatable;
            Name = name;
            IsUnique = isUnique;
            Description = description;
            Cooldown = cooldown;
            CooldownTimer = Cooldown.HasValue ? new Stopwatch() : null;
        }

        #endregion

        #region Methods

        public void ActivateAbility(IUnit unit)
        {
            if (!IsActivatable) return;
            if (IsOnCooldown()) return;
            AbilityThread = new Thread(_ => AbilityMethod(unit));
            AbilityThread.Start();
        }

        private void AbilityMethod(IUnit unit)
        {
            CooldownTimer.Start();
            Effect.ActivateEffect(unit);
            if (Cooldown.HasValue)
                Task.Delay(Cooldown.Value).ContinueWith(_ => CooldownTimer.Reset());
        }

        public bool IsOnCooldown()
        {
            return CooldownTimer.Elapsed != TimeSpan.Zero && CooldownTimer.IsRunning;
        }

        public TimeSpan RemainingCooldown
        {
            get
            {
                if (Cooldown != null)
                {
                    return !IsOnCooldown()
                        ? TimeSpan.Zero
                        : TimeSpan.FromMilliseconds(Cooldown.Value.TotalMilliseconds -
                                                    CooldownTimer.Elapsed.TotalMilliseconds);
                }
                return TimeSpan.Zero;
            }
        }

        #endregion
    }
}