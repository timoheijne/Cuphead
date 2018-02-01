using UnityEngine;

// Created by Timo Heijne
public class DeathExplosionSpawner : MonoBehaviour {
    [SerializeField]
    public GameObject effect;

    public static DeathExplosionSpawner instance;

    void Start() {
        instance = this;
    }

    public void SpawnAtPoint(Vector3 position) {
        Destroy(Instantiate(effect, position, Quaternion.identity), 2);
    }
}