using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Created By Timo Heijne
public class GatePaperAttack : MonoBehaviour {
    private Transform _player;
    public float speed = 1.0F;
    private float _startTime;
    private float _journeyLength;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private ParticleSystem _particleSystem;

    // Use this for initialization
    void Start() {
        // We Spawn and we move outside of the screen and continue a rush to the players position and destroy
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _startPosition = transform.position;
        _endPosition = _player.transform.position;
        _endPosition.y = _startPosition.y;
        _journeyLength = Vector3.Distance(_startPosition, _endPosition);
        _startTime = Time.time;

        _particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {
        float distCovered = (Time.time - _startTime) * speed;
        float fracJourney = distCovered / _journeyLength;
        transform.position = Vector3.Lerp(transform.position, _endPosition, fracJourney);

        if (Vector3.Distance(transform.position, _endPosition) <= 0.1f) {
            _particleSystem.Stop();
            Destroy(gameObject, 2);
        }
    }
}