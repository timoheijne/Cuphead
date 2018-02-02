using UnityEngine;

namespace GameModifiers.Modifiers
{
    /// <summary>
    /// Binds self to camera and rotates it at a constant (or random)
    /// speed
    /// </summary>
    public class JumpDelayModifier : Modifier
    {
        private PlayerMovement _movement;
        private PlayerInput _input;

        private float timer = 0;
        private float jumpTime = 0.2f;
        private const float minJumpTime = 0.2f;
        private const float maxJumpTime = 1.6f;
        
        public override void StartMod(Randomiser r)
        {
            PlayerInput.jumpdelay = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _movement = player.GetComponent<PlayerMovement>();
            _input = player.GetComponent<PlayerInput>();

        }

        public override void UpdateMod(Randomiser r)
        {
            if (timer <= 0.001f && _input.Jump)
            {
                timer = Random.Range(minJumpTime, maxJumpTime);
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer < 0 && _movement.CanJump)
                {
                    _movement.Jump();
                }
            }
        }

        public override void DestroyMod(Randomiser r)
        {
            PlayerInput.jumpdelay = false;
        }

        public JumpDelayModifier(string name) : base(name)
        {
        }
    }
}