using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne

/// <summary>
/// Handle the spawning & despawning of the spikes.
/// </summary>
public class GateSpikes : MonoBehaviour {
    public GameObject spike1;
    public GameObject spike2;
    private Transform _player;

    [Tooltip(
        "By default a spike spawns on the player, and since we spawn two we don't want to have then 'collide' set the offset here")]
    [SerializeField]
    private Vector3 spike1Offset;

    [Tooltip(
        "By default a spike spawns on the player, and since we spawn two we don't want to have then 'collide' set the offset here")]
    [SerializeField]
    private Vector3 spike2Offset;

    // Use this for initialization
    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnSpikes() {
        float yoverride = -0.5f;
        Vector3 s1Target = _player.transform.position;
        Vector3 s2Target = _player.transform.position;
        s1Target.y = yoverride;
        s2Target.y = yoverride;

        GameObject s1 = Instantiate(spike1, s1Target + spike1Offset, Quaternion.identity);
        GameObject s2 = Instantiate(spike2, s2Target + spike2Offset, Quaternion.identity);

        Destroy(s1, 3);
        Destroy(s2, 3);
    }
}