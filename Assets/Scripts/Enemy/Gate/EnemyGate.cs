using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Timo Heijne
[RequireComponent(typeof(GateAnimation))]
public class EnemyGate : MonoBehaviour {

	private GameObject _player;
	private GateAnimation _animator;

	public enum GateStatus {
		Spikes,
		Trash,
		Smash,
		SmashRecover,
		Idle,
		Dead
	}

	public GateStatus state { get; private set; }
	public float attackDelay = 5;
	private bool isAttacking = false;
	private float attackTimer = 0;
		
	// Use this for initialization
	void Start () {
		if(!_player)
			_player = GameObject.FindGameObjectWithTag("Player");

		_animator = GetComponent<GateAnimation>();
		state = GateStatus.Idle;
	}
	
	// Update is called once per frame
	void Update () {
		_animator.SetState(state);
		attackTimer -= Time.deltaTime;
		if (!isAttacking && attackTimer <= 0) {
			Reason();
		}	
	}

	/// <summary>
	/// This function decides which attack the Gate should use
	/// </summary>
	void Reason() {
		attackTimer = attackDelay; // i = defaultI
		// Attacks contains of Spikes, Crap out of gate & smash
		Debug.Log("EnemyGate :: Reason Called");
		// Smash should be used when player is near the gate
		// Spikes & Trash can be used anytime also we prefer smash over trash & spoikes
		if ((Vector3.Distance(_player.transform.position, transform.position) < 10)) {
			// SMASH
			state = GateStatus.Smash;
			Debug.Log("EnemyGate :: Smashing (https://i.imgur.com/F2IrisG.jpg)");
		}
		else {
			// No smash :(
			int random = Mathf.FloorToInt(Random.Range(0, 2));
			Debug.Log("EnemyGate :: Random Attack: " + random);
			if (random == 1) {
				AttackSpike();
				state = GateStatus.Spikes;
			}
			else {
				AttackTrash();
				state = GateStatus.Trash;
			}
		}
		state = GateStatus.Smash; // Debug thingy
		_animator.SetState(state);
	}

	void AttackSmash() {
		
	}

	IEnumerator WaitForRecoverTime() {
		yield return new WaitForSeconds(2f);
		state = GateStatus.Idle;
		_animator.SetState(state);
		_animator.Trigger("smashRecover");
	}

	void AttackTrash() {
		// Open gate and send trash through gate
	}

	void AttackSpike() {
		// Make the gate shit a spike
		
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (state == GateStatus.Smash) {
			// Player has been hit man.. send message to player's health script
		}
	}
}
