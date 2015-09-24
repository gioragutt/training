namespace ModuloFramework.AbilitySystem.Effects
{
    public abstract class ModifyUnitEffect : IEffect
    {
        public abstract void ModifyUnit(IUnit unit);
        public abstract bool CanModifyUnit(IUnit unit);

        public void ActivateEffect(IUnit unit)
        {
            if (CanModifyUnit(unit))
                ModifyUnit(unit);
        }
    }
}
