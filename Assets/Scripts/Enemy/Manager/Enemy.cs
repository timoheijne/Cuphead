using UnityEngine;

// Created By Timo Heijne
[System.Serializable]
public class Enemy {
    public string name;
    public GameObject prefab;
    public float health = 100;

    [HideInInspector] public int killed = 0; // Times this boss has been killed

    [HideInInspector] public Health healthScript;

    [HideInInspector] public GameObject activeGameObject;
}