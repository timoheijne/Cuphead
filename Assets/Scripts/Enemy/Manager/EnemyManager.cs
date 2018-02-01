using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

/// Created By Timo Heijne
/// <summary>
/// This script keeps track of our enemies. Also when in crazy mode how many kills one got
/// </summary>
public class EnemyManager : MonoBehaviour {

	public static EnemyManager instance;

	public Enemy[] enemies;
	private Enemy activeEnemy;
	
	public enum Gamemodes {
		Normal,
		Crazy
	}
	public Gamemodes mode= Gamemodes.Normal;
	
	// Use this for initialization
	void Start () {
		if (EnemyManager.instance != null) {
			Destroy(gameObject);
		} else {
			SpawnEnemies();
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void SpawnEnemies() {
		foreach (var enemy in enemies) {
			if (enemy.activeGameObject == null) {
				enemy.activeGameObject = Instantiate(enemy.prefab);
				enemy.healthScript = enemy.activeGameObject.GetComponent<Health>();
				enemy.healthScript.CurHealth = enemy.health;
				enemy.activeGameObject.name = enemy.name;
				enemy.activeGameObject.SetActive(false);

				enemy.healthScript.HasDied += RegisterDeath;
			}
		}
		
		if(enemies.Length > 0)
			SetActiveEnemy(enemies[0]);
		
	}

	public void SetMode(Gamemodes newMode) {
		mode = newMode;
	}

	private void RegisterDeath() {
		Debug.Log("Death Registered");
		activeEnemy.killed += 1;
		for (int i = 0; i < enemies.Length; i++) {
			Debug.Log(enemies[i].name);
			if (enemies[i].name == activeEnemy.name) {
				// Why don't we have IndexOf?
				if (i == enemies.Length - 1) {
					if (mode == Gamemodes.Normal) {
						Debug.Log("Last Reached - Normal Mode");
						// Reached last enemy.. We done boi
						activeEnemy.activeGameObject.SetActive(false);
						
					} else {
						Debug.Log("Roight Next one");

						SetActiveEnemy(enemies[0]);
					}
				} else {
					Debug.Log("Roight Next one");
					SetActiveEnemy(enemies[i+1]);
				}

				break;
			}
		}
	}

	private void SetActiveEnemy(Enemy enemy) {
		if (activeEnemy != null) StartCoroutine(DisableObject(activeEnemy)); // We do this so we can allow some animations & explosions or whatever

		StartCoroutine(ActivateObject(enemy));
	}

	IEnumerator DisableObject(Enemy enemy) {
		yield return new WaitForSeconds(2f);
		EffectManager.instance.SpawnExplosionAtPoint(activeEnemy.activeGameObject.transform.position);
		activeEnemy.activeGameObject.SetActive(false);
	}

	IEnumerator ActivateObject(Enemy enemy) {
		yield return new WaitForSeconds(2.5f);
		
		activeEnemy = enemy;
		activeEnemy.healthScript.CurHealth = activeEnemy.health;
		activeEnemy.healthScript.dead = false;
		activeEnemy.activeGameObject.SetActive(true);
	}

	public void ResetStats() {
		foreach (var enemy in enemies) {
			enemy.killed = 0;
			enemy.healthScript.CurHealth = enemy.health;
		}
	}
}
