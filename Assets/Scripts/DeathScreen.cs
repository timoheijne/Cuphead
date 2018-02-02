using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Created by Timo Heijne
public class DeathScreen : MonoBehaviour {

	private GameObject _player;
	[SerializeField] private Slider _percentageSlider;

	void Awake() {
		EnemyManager.onGameWon += OnGameWon;
		_player = GameObject.FindGameObjectWithTag("Player");
		_player.GetComponent<PlayerHealth>().HasDied += HasDied;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.K)) {
			CalculatePercentage();
			ActivateDeathOverlay();
		}
	}

	private void HasDied() {
		// The player has died
		CalculatePercentage();
		ActivateDeathOverlay();
	}

	private void OnGameWon() {
		// Game is in normal mode & all bosses are ded
		_percentageSlider.value = 1; // We already know this since the player has won.
		ActivateDeathOverlay();
	}

	void ActivateDeathOverlay() {
		Time.timeScale = 0;
		transform.GetChild(0).gameObject.SetActive(true);
	}

	public void Retry() {
		if (EnemyManager.instance.mode == EnemyManager.Gamemodes.Normal) {
			SceneManager.LoadScene("Timo's Work Scene");
		}
		else {
			// Some logic for antiono to put here 'n stuff
		}
	}

	public void Quit() {
		SceneManager.LoadScene("mainmenu");
	}

	void CalculatePercentage() {
		EnemyManager em = EnemyManager.instance;
		float totalHealth = 0;
		float totalDamageDone = 0;
		foreach (var enemies in em.enemies) {
			totalHealth += enemies.health;
			totalDamageDone += (enemies.health - enemies.healthScript.CurHealth);
		}

		float percentage = (100 / totalHealth * totalDamageDone) / 100;
		_percentageSlider.value = percentage;
	}
}
