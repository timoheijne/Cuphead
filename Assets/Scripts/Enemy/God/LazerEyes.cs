using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timoheijne
/// <summary>
/// This handels the laZer eye's of god (AND YES LAZER IS THE SUPPERIOR WAY OF SPELLING "LASER"..) 
/// </summary>
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
	}

	public IEnumerator RunLaser() {
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

			if (fracJourney >= 1.5) {
				_lineRenderer.SetPositions(new Vector3[] {Vector3.zero, Vector3.zero});
				yield break;
			}
			
			yield return new WaitForEndOfFrame();
		}
				
		
	}

	public void StartLaser() {
		StartCoroutine(RunLaser());
	}
}
