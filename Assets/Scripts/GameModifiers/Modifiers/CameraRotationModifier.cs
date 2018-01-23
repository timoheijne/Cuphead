using UnityEngine;

namespace GameModifiers.Modifiers
{
    /// <summary>
    /// Binds self to camera and rotates it at a constant (or random)
    /// speed
    /// </summary>
    public class CameraRotationModifier : Modifier
    {
        private GameObject _camera;
        
        public override void StartMod(Randomiser r)
        {
            _camera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        public override void UpdateMod(Randomiser r)
        {
            _camera.transform.Rotate(0, 0, 10 * Time.deltaTime);
        }

        public override void DestroyMod(Randomiser r)
        {
            _camera.transform.rotation = Quaternion.identity;
        }
    }
}