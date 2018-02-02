using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne
public class AngelArrow : MonoBehaviour {
    public float speed = 5;

    // Update is called once per frame
    void Update() {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision other) {
        throw new System.NotImplementedException();
    }
}