namespace MonoGameFirst.BaseGameClasses.Player_Classes
{
    public class PlayerStats
    {
        #region Data Members

        private int maxHealth;

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the player max health
        /// Restrictions: MAX_HEALTH >= 1
        /// </summary>
        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value <= 0 ? 0 : value; }
        }

        /// <summary>
        /// Gets and sets the player movespeed
        /// </summary>
        public int MoveSpeed { get; set; }

        #endregion Properties

        #region Constructor

        private PlayerStats() { }

        public static PlayerStats Create(int maxHealth = 0, int movespeed = 0)
        {
            return new PlayerStats()
            {
                MaxHealth = maxHealth,
                MoveSpeed = movespeed
            };
        }

        #endregion

        #region Operator Overload

        public static PlayerStats operator+(PlayerStats lhs, PlayerStats rhs)
        {
            return new PlayerStats
            {
                MaxHealth = lhs.MaxHealth + rhs.MaxHealth,
                MoveSpeed = lhs.MoveSpeed + rhs.MoveSpeed,
            };
        }

        #endregion
    }
}
