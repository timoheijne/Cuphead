using UnityEngine;

// Created By Timo Heijne
/// <summary>
/// This is our datatype for enemies. so we can very easily add more enemies to the cycle if we want to
/// </summary>
[System.Serializable]
public class Enemy {
    public string name;
    public GameObject prefab;
    public float health = 100;

    [HideInInspector] public int killed = 0; // Times this boss has been killed

    [HideInInspector] public Health healthScript;

    [HideInInspector] public GameObject activeGameObject;
}