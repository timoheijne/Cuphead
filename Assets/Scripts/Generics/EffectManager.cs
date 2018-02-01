using UnityEngine;

// Created by Timo Heijne
public class EffectManager : MonoBehaviour {
    public GameObject explosionEffect;
    public GameObject projectileHit;

    public static EffectManager instance;

    void Start() {
        instance = this;
    }

    public void SpawnExplosionAtPoint(Vector3 position) {
        Destroy(Instantiate(explosionEffect, position, Quaternion.identity), 2);
    }

    public void SpawnHitAtPoint(Vector3 position) {
        Destroy(Instantiate(projectileHit, position, Quaternion.identity), 1);
    }
}