namespace GameModifiers.Modifiers
{
    public class LowRangeModifier : Modifier
    {
        public LowRangeModifier(string name) : base(name) { }

        public override void StartMod(Randomiser r)
        {
            Projectile.shortLifeSpan = true;
        }

        public override void UpdateMod(Randomiser r)
        {
            
        }

        public override void DestroyMod(Randomiser r)
        {
            Projectile.shortLifeSpan = false;
        }
    }
}