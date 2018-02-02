using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne
/// <summary>
/// Angel Arrow.....
/// </summary>
public class AngelArrow : MonoBehaviour {
    public float speed = 5;

    // Update is called once per frame
    void Update() {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}