using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEyes : MonoBehaviour {
	private LineRenderer _lineRenderer;

	[SerializeField]
	private Transform _startLocation;

	[SerializeField]
	private LayerMask _layerMask;

	public float speed = 1.0F;
	private float _startTime;
	private float _journeyLength;
	
	// Use this for initialization
	void Start () {
		_lineRenderer = GetComponent<LineRenderer>();

		StartCoroutine(RunLaser());
	}

	IEnumerator RunLaser() {
		yield return new WaitUntil(() => Boss._player != null);
		
		_startTime = Time.time;
		_journeyLength = Vector3.Distance(_startLocation.position, Boss._player.transform.position);
		
		bool Buildup = true;

		while (Buildup) {
			GameObject _player = Boss._player;
			CapsuleCollider2D _plyCollider = _player.GetComponent<CapsuleCollider2D>();

			Vector3 dir = (_player.transform.position - _startLocation.position);

			RaycastHit2D hit = Physics2D.Raycast(_startLocation.position, dir, Mathf.Infinity, _layerMask);
			if (hit.collider != null) {
				print("Found an object: " + hit.transform.name);
				_journeyLength = Vector3.Distance(_startLocation.position, hit.point);
			}		
			
			float distCovered = (Time.time - _startTime) * speed;
			float fracJourney = distCovered / _journeyLength;
			Vector3 endPos = Vector3.Lerp(_startLocation.position, hit.point, fracJourney);
			
			_lineRenderer.SetPositions(new Vector3[] {endPos, _startLocation.position} );

			if (fracJourney >= 1)
				Buildup = false;
			
			yield return new WaitForEndOfFrame();
		}
				
		_lineRenderer.SetPositions(new Vector3[] {Vector3.zero, Vector3.zero});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartLaser(Transform target) {
		
	}

	public void StopLaser() {
		
	}
}
