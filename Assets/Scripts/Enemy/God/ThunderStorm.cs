using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Timo Heijne
public class ThunderStorm : MonoBehaviour {

	public AudioClip audioClip;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(LightingStrike());
	}

	IEnumerator LightingStrike() {
		yield return new WaitForSeconds(2f);
		AudioSource.PlayClipAtPoint(audioClip, transform.position);
		transform.GetChild(0).gameObject.SetActive(true);
		yield return new WaitForSeconds(0.15f);
		Destroy(gameObject);
	}
}
