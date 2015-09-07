using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameFirst.BaseGameClasses.Player_Classes.Stat_Classes
{
    public class RawBonus : BaseStat
    {
        public RawBonus(float baseValue = 0, float baseMultiplier = 0) 
            : base(baseValue, baseMultiplier) { }
    }

    public class FinalBonus : BaseStat
    {
        public FinalBonus(float baseValue = 0, float baseMultiplier = 0) 
            : base(baseValue, baseMultiplier) { }
    }
}
