namespace GameModifiers.Modifiers
{
    public class InvertedControlsModifier : Modifier
    {
        public override void StartMod(Randomiser r)
        {
            PlayerInput.inverted = true;
        }

        public override void UpdateMod(Randomiser r)
        {
        }

        public override void DestroyMod(Randomiser r)
        {
            PlayerInput.inverted = false;
        }
    }
}