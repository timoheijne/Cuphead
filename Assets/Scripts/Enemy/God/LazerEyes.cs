using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne
/// <summary>
/// This handels the laZer eye's of god
/// </summary>
public class LazerEyes : MonoBehaviour {
    private LineRenderer _lineRenderer;

    [SerializeField] private Transform _startLocation;
    [SerializeField] private LayerMask _layerMask;

    public float speed = 1.0F;
    private float _startTime;
    private float _journeyLength;
    private Vector3 _endLocation;

    // Use this for initialization
    void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public IEnumerator RunLaser() {
        yield return new WaitUntil(() => Boss.player != null);

        _startTime = Time.time;
        _journeyLength = Vector3.Distance(_startLocation.position, _endLocation);

        while (true) {
            GameObject player = Boss.player;

            Vector3 dir = (_endLocation - _startLocation.position);

            RaycastHit2D hit = Physics2D.Raycast(_startLocation.position, dir, Mathf.Infinity, _layerMask);
            if (hit.collider != null) {
                print("Found an object: " + hit.transform.name);
                _journeyLength = Vector3.Distance(_startLocation.position, hit.point);
            }

            float distCovered = (Time.time - _startTime) * speed;
            float fracJourney = distCovered / _journeyLength;
            Vector3 endPos = Vector3.Lerp(_startLocation.position, hit.point, fracJourney);

            _lineRenderer.SetPositions(new Vector3[] {endPos, _startLocation.position});

            if (fracJourney >= 1.5) {
                if (hit.transform.CompareTag("Player")) {
                    Boss.player.GetComponent<PlayerHealth>().TakeDamage();
                }
                
                _lineRenderer.SetPositions(new Vector3[] {Vector3.zero, Vector3.zero});
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void StartLaser() {
        _endLocation = Boss.player.transform.position;
        StartCoroutine(RunLaser());
    }
}