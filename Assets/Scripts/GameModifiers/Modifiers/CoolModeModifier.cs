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
        private GameObject _backdropPrefab;
        private GameObject _backdrop;
        private AudioClip _explosionSound;

        private RGBSplit _rgbSplit;

        private const float shakeStrengthPerHit = 0.2f;
        private const float shakeStrengthDecay = 2;
        private float shakeStrength;
        private float _originalRGBSplitStrength;

        private Vector3 originalCamPos;
        
        public override void StartMod(Randomiser r)
        {
            Projectile.OnProjectileHit += OnProjectileHit;
            _explosionParticle = Resources.Load<GameObject>("explosion");
            _backdropPrefab = Resources.Load<GameObject>("coolmode environment");
            _backdrop = GameObject.Instantiate(_backdropPrefab, Vector3.zero, Quaternion.identity);
            _explosionSound = Resources.Load<AudioClip>("explosion");
            _rgbSplit = Camera.main.GetComponent<RGBSplit>();
            _originalRGBSplitStrength = _rgbSplit.offset;
            
            originalCamPos = Camera.main.transform.position;
        }

        private void OnProjectileHit(Vector3 eventArgs)
        {
            Object.Instantiate(_explosionParticle, eventArgs, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            AudioSource.PlayClipAtPoint(_explosionSound, eventArgs);
            shakeStrength += shakeStrengthPerHit;
        }

        public override void UpdateMod(Randomiser r)
        {
            if (shakeStrength > 0)
            {
                shakeStrength -= shakeStrengthDecay * Time.deltaTime;
                _rgbSplit.offset = _originalRGBSplitStrength + shakeStrength * 10;
                Camera.main.transform.position = originalCamPos + (Vector3)Random.insideUnitCircle * shakeStrength;
            }
            else
            {
                _rgbSplit.offset = _originalRGBSplitStrength;
                Camera.main.transform.position = originalCamPos;
            }
        }

        void DeleteAmmoKit(GameObject ammokit)
        {
            
        }

        public override void PlayMusic()
        {
            MusicManager.Instance.PlayCoolSong();

        }

        public override void DestroyMod(Randomiser r)
        {
            Projectile.OnProjectileHit -= OnProjectileHit;
            Object.Destroy(_backdrop);
        }

        public CoolModeModifier(string name) : base(name) {}
    }
}