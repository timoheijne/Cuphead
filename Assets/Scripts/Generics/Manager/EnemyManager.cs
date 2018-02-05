using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// Created By Timo Heijne
/// <summary>
/// This script keeps track of our enemies. Also when in crazy mode how many kills one got
/// </summary>
public class EnemyManager : MonoBehaviour {
    public static EnemyManager instance;

    public static BossKilled OnBossKilled;

    public delegate void BossKilled(Enemy enemy);

    public Enemy[] enemies;
    private Enemy _activeEnemy;

    public enum Gamemodes {
        Normal,
        Crazy
    }

    public Gamemodes mode = Gamemodes.Normal;

    public static UnityAction onGameWon;

    // Use this for initialization
    void Awake() {
        if (EnemyManager.instance != null) {
            Destroy(gameObject);
        } else {
            SpawnEnemies();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += CheckIfMainMenu;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= CheckIfMainMenu;
    }

    private void CheckIfMainMenu(Scene s, LoadSceneMode e)
    {
        if (s.buildIndex == 0)
        {
            Destroy(gameObject);
            SceneManager.sceneLoaded -= CheckIfMainMenu;
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

        if (enemies.Length > 0) SetActiveEnemy(enemies[0]);
    }

    public void SetMode(Gamemodes newMode) {
        mode = newMode;
    }

    private void RegisterDeath() {
        Debug.Log("Death Registered");
        _activeEnemy.killed += 1;

        if (OnBossKilled != null) OnBossKilled(_activeEnemy);
        
        for (int i = 0; i < enemies.Length; i++) {
            Debug.Log(enemies[i].name);
            if (enemies[i].name == _activeEnemy.name) {
                // Why don't we have IndexOf?
                if (i == enemies.Length - 1) {
                    if (mode == Gamemodes.Normal) {
                        Debug.Log(enemies[i].name + " - Normal Mode - Last Reached");
                        // Reached last enemy.. We done boi
                        StartCoroutine(GameWon(_activeEnemy));
                    } else {
                        Debug.Log(enemies[i].name + " - Crazy Mode - Last One So Go Back To First");

                        SetActiveEnemy(enemies[0]);
                    }
                } else {
                    Debug.Log(enemies[i].name + " - Not Last One Continue To Next One");
                    SetActiveEnemy(enemies[i + 1]);
                }

                break;
            }
        }
    }

    private void SetActiveEnemy(Enemy enemy) {
        if (_activeEnemy != null)
            StartCoroutine(
                DisableObject(_activeEnemy)); // We do this so we can allow some animations & explosions or whatever

        StartCoroutine(ActivateObject(enemy));
    }

    IEnumerator GameWon(Enemy enemy) {
        yield return new WaitForSeconds(2f);
        EffectManager.instance.SpawnExplosionAtPoint(_activeEnemy.activeGameObject.transform.position);
        _activeEnemy.activeGameObject.SetActive(false);

        if (onGameWon != null) onGameWon();
    }

    IEnumerator DisableObject(Enemy enemy) {
        yield return new WaitForSeconds(2f);
        EffectManager.instance.SpawnExplosionAtPoint(_activeEnemy.activeGameObject.transform.position);
        _activeEnemy.activeGameObject.SetActive(false);
    }

    IEnumerator ActivateObject(Enemy enemy) {
        yield return new WaitForSeconds(2.5f);

        _activeEnemy = enemy;
        _activeEnemy.healthScript.CurHealth = _activeEnemy.health;
        _activeEnemy.healthScript.dead = false;
        _activeEnemy.activeGameObject.SetActive(true);
    }

    public void SpawnFirstEnemy() {
        StartCoroutine(ActivateObject(enemies[0]));
    }

    public void ResetStats() {
        foreach (var enemy in enemies) {
            enemy.killed = 0;
            enemy.healthScript.CurHealth = enemy.health;
        }
    }
}