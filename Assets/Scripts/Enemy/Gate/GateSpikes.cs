using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSpikes : MonoBehaviour {

	public GameObject spike1;
	public GameObject spike2;
	private Transform _player;

	[SerializeField] private Vector3 spike1Offset;
	[SerializeField] private Vector3 spike2Offset;
	
	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public void SpawnSpikes() {
		GameObject s1 = Instantiate(spike1, _player.transform.position + spike1Offset, Quaternion.identity);
		Vector3 spike2Target = _player.transform.position;
		spike2Target.x -= 1;
		GameObject s2 = Instantiate(spike2, spike2Target + spike2Offset, Quaternion.identity);
		
		Destroy(s1, 5);
		Destroy(s2, 5);
	}
}
