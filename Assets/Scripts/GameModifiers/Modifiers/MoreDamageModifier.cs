namespace GameModifiers.Modifiers
{
    public class MoreDamageModifier : Modifier
    {
        public MoreDamageModifier(string name) : base(name)
        {
        }

        private const float NEW_DAMAGE = 4;
        private float oldDamageTaken;
        
        public override void StartMod(Randomiser r)
        {
            oldDamageTaken = Health.DamageTaken;
            Health.DamageTaken = NEW_DAMAGE;
        }

        public override void UpdateMod(Randomiser r)
        {
            
        }

        public override void DestroyMod(Randomiser r)
        {
            Health.DamageTaken = oldDamageTaken;
        }
    }
}