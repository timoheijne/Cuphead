using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Created By Timo Heijne
public class GateSmash : MonoBehaviour {
    private EnemyGate _enemyGate;

    // Use this for initialization
    void Start() {
        _enemyGate = transform.parent.GetComponent<EnemyGate>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _enemyGate.isPlayerInSmashRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _enemyGate.isPlayerInSmashRadius = false;
        }
    }
}