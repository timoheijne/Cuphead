using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne
/// <summary>
/// When the player goes out of screen it falls of the edge.. and as something to make the game slightly harder we decided the guy should die.
/// </summary>
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
