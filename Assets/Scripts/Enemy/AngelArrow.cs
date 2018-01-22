using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelArrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime;
	}

	private void OnCollisionEnter(Collision other) {
		throw new System.NotImplementedException();
	}
}
