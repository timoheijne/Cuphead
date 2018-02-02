using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne
public class PlayerGameBorder : MonoBehaviour {

	private PlayerHealth _playerHealth;
	
	// Use this for initialization
	void Start () {
		_playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			_playerHealth.Die();
		}
 	}
}
