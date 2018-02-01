using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Timo Heijne
/// <summary>
/// Here we decide what attack the gate should do, we also handle what should happen when the gate is ded
/// </summary>
[RequireComponent(typeof(GateAnimation))]
public class EnemyGate : MonoBehaviour {


	public GameObject trashObject;
	
	private GameObject _player;
	private GateAnimation _animator;
	private Health _health;

	public bool isPlayerInSmashRadius = false; 

	public enum GateStatus {
		Spikes,
		Trash,
		Smash,
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
		_health = GetComponent<Health>();
		state = GateStatus.Idle;

		_health.HasDied += OnDeath;
	}

	void OnDeath() {
		state = GateStatus.Dead;
		_animator.SetState(state);
	}
	
	// Update is called once per frame
	void Update () {
		//_animator.SetState(state);
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
		if ((Vector3.Distance(_player.transform.position, transform.position) < 5)) {
			// SMASH
			state = GateStatus.Smash;
			Debug.Log("EnemyGate :: Smashing (https://i.imgur.com/F2IrisG.jpg)");
		}
		else {
			// No smash :(
			int random = Mathf.FloorToInt(Random.Range(0, 2));
			Debug.Log("EnemyGate :: Random Attack: " + random);
			if (random == 1) {
				state = GateStatus.Spikes;
			}
			else {
				state = GateStatus.Trash;
			}
		}
		
		_animator.SetState(state);
	}

	void AttackSmash() {
		if (isPlayerInSmashRadius) {
			Debug.Log("EnemyGate :: Player Hit");
			_player.GetComponent<PlayerHealth>().TakeDamage();
		}
		StartCoroutine(WaitForRecoverTime());
	}

	IEnumerator WaitForRecoverTime() {
		yield return new WaitForSeconds(1f);
		state = GateStatus.Idle;
		_animator.SetState(state);
		_animator.Trigger("smashRecover");
	}

	void AttackTrash() {
		// Gate is open send the trash through
		Vector3 pos = Camera.main.transform.position;
		Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));
		pos.x = p.x + 5;
		pos.z = 0;

		GameObject go = Instantiate(trashObject, pos, trashObject.transform.rotation);
	}
}
