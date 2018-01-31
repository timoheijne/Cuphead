using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace GameModifiers.Modifiers
{
    public class CoolModeModifier : Modifier
    {
        private GameObject _explosionParticle;
        
        public override void StartMod(Randomiser r)
        {
            Projectile.OnProjectileHit += OnProjectileHit;
            
            _explosionParticle = Resources.Load<GameObject>("explosion");
        }

        private void OnProjectileHit(Vector3 eventArgs)
        {
            GameObject.Destroy(
                Object.Instantiate(_explosionParticle, eventArgs, Quaternion.Euler(0, 0, Random.Range(0, 360))), 5);
        }

        public override void UpdateMod(Randomiser r)
        {
            
        }

        void DeleteAmmoKit(GameObject ammokit)
        {
            
        }

        public override void DestroyMod(Randomiser r)
        {
            Projectile.OnProjectileHit -= OnProjectileHit;
        }

        public CoolModeModifier(string name) : base(name) {}
    }
}