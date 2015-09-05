using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameFirst.BaseGameClasses
{
    public class PlayerStats
    {
        #region Data Members

        private int m_health;
        private int m_maxHealth;

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the player health
        /// Restrictions: 0 <= HEALTH <= MAX_HEALTH
        /// </summary>
        public int Health
        {
            get
            {
                return m_health;
            }
            set
            {
                if (value < 0)
                    m_health = 0;
                else if (value > MaxHealth)
                    m_health = MaxHealth;
                else
                    m_health = value;                       
            }
        }

        /// <summary>
        /// Gets and sets the player max health
        /// Restrictions: MAX_HEALTH >= 1
        /// </summary>
        public int MaxHealth
        {
            get
            {
                return m_maxHealth;
            }
            set
            {
                if (value <= 0)
                    m_maxHealth = 1;
                else
                    m_maxHealth = value;                    
            }
        }

        /// <summary>
        /// Gets and sets the player movespeed
        /// </summary>
        public int MoveSpeed
        {
            get; set;
        }

        /// <summary>
        /// Gets whether the player is dead or not (dead => hp = 0)
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return Health == 0;
            }
        }

        public float PercentHealth
        {
            get
            {
                return (float)Health / MaxHealth;
            }                
        }

        #endregion Properties
    }
}
