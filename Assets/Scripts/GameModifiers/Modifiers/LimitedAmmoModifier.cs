using System;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace GameModifiers.Modifiers
{
    public class LimitedAmmoModifier : Modifier
    {
        private PlayerShooting _ps;
        private PlayerInput _pi;
        private GameObject _ammoKit;
        

        private float ammoKitEvery = 5; // seconds
        private float ammoKitLastSpawnTime;

        private int currentAmmo = 50;

        private List<GameObject> SpawnedAmmoKits;
        
        public override void StartMod(Randomiser r)
        {
            _ps = r.PLAYER.GetComponent<PlayerShooting>();
            _pi = r.PLAYER.GetComponent<PlayerInput>();
            _ammoKit = Resources.Load<GameObject>("ammokit");
            
            ammoKitLastSpawnTime = Time.time;
            
            PlayerShooting.OnShoot += OnShoot;
            PlayerShooting.limitedAmmoMode = true;

            SpawnedAmmoKits = new List<GameObject>();
        }

        private void OnShoot(object sender, EventArgs eventArgs)
        {
            currentAmmo--;
        }

        public override void UpdateMod(Randomiser r)
        {
            foreach (var ammokit in SpawnedAmmoKits)
            {
                // move to the left 
                var pos = ammokit.transform.position;
                pos.x -= 2 * Time.deltaTime;
                ammokit.transform.position = pos;
                
                // check distance from player
                Vector3 ppos = r.PLAYER.transform.position;
                ppos.z = 0;
                Vector3 apos = ammokit.transform.position;
                apos.z = 0;
                float distance = (ppos - apos ).magnitude;
                const float minDistance = 1f;
                Debug.Log(distance);
                if (distance > minDistance) continue;
                
                currentAmmo += 25;
                DeleteAmmoKit(ammokit);
                break;
            }
            
            if(Time.time > ammoKitLastSpawnTime + ammoKitEvery) SpawnAmmoKit();

            if (_pi.Shoot && currentAmmo > 0 && _ps.CanShoot) _ps.Shoot();
        }

        public override void DestroyMod(Randomiser r)
        {
            PlayerShooting.OnShoot -= OnShoot;
            PlayerShooting.limitedAmmoMode = false;
        }

        void DeleteAmmoKit(GameObject ammokit)
        {
            if (!SpawnedAmmoKits.Remove(ammokit)) return;
            Object.Destroy(ammokit);
        }

        void SpawnAmmoKit()
        {
            float height = Camera.main.orthographicSize;
            float width = height * 2 / Screen.height * Screen.width + 2;
            Vector3 spawnposition = new Vector3(width, Random.Range(-height, height));
            var ammokit = Object.Instantiate(_ammoKit, spawnposition, Quaternion.identity);
            SpawnedAmmoKits.Add(ammokit);
            ammoKitLastSpawnTime = Time.time;
        }

        public LimitedAmmoModifier(string name) : base(name)
        {
            
        }
    }
}