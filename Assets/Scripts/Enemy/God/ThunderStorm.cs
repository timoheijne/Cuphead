using System.Collections;
using UnityEngine;

// Created by Timo Heijne
public class ThunderStorm : MonoBehaviour {

	public AudioClip audioClip;
	private Vector3 targetPos;
	private bool hasInitialized = false;

	[SerializeField] private Vector3 targetScale;
	
	// Use this for initialization
	void Start () {
		targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		targetPos.y = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane)).y;
	}


	void Update() {
		transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 2);
		transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 2);

		if (Vector3.Distance(transform.position, targetPos) <= 0.1f && !hasInitialized) {
			hasInitialized = true;
			StartCoroutine(LightingStrike());
		}
	}

	IEnumerator LightingStrike() {
		yield return new WaitForSeconds(1.5f);
		AudioSource.PlayClipAtPoint(audioClip, transform.position);
		transform.GetChild(0).gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}

