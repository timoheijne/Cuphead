using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne
public class AngelArrow : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime;
	}

	private void OnCollisionEnter(Collision other) {
		throw new System.NotImplementedException();
	}
}
