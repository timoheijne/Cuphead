namespace GameModifiers.Modifiers
{
    public class FasterBulletsModifier : Modifier
    {
        public FasterBulletsModifier(string name) : base(name) {}

        private const float NEW_SPEED = 30;
        
        private float oldSpeed;
        private float oldTime;

        private PlayerSound playerSound;
        
        public override void StartMod(Randomiser r)
        {
            oldSpeed = Projectile.Speed;
            Projectile.Speed = NEW_SPEED;
            oldTime = PlayerShooting.SHOOT_TIME;
            PlayerShooting.SHOOT_TIME /= 3;

            playerSound = r.PLAYER.GetComponent<PlayerSound>();
            playerSound.a_shootingLoop.pitch = 2;
        }

        public override void UpdateMod(Randomiser r)
        {
            
        }

        public override void DestroyMod(Randomiser r)
        {
            Projectile.Speed = oldSpeed;
            PlayerShooting.SHOOT_TIME = oldTime;
            playerSound.a_shootingLoop.pitch = 1;
        }
    }
}