using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameFirst.BaseGameClasses
{
    public class PlayerStats
    {
        #region Properties

        public int Health
        {
            get; set;
        }

        public int MoveSpeed
        {
            get; set;
        }

        public bool IsAlive
        {
            get
            {
                return Health == 0;
            }
        }

        #endregion Properties
    }
}
