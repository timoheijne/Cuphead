using System.Collections;
using UnityEngine;

// Created by Timo Heijne
public class ThunderStorm : MonoBehaviour {
    public AudioClip audioClip;
    private Vector3 targetPos;
    private bool hasInitialized = false;
    private Transform _player;
    private Health _health;
    [SerializeField] private Vector3 targetScale;

    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _health = GetComponent<Health>();
        _health.HasDied += OnDeath;
    }

    void Update() {
        targetPos = _player.position;
        targetPos.y = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane)).y;
        
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 2);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 2);

        if (Vector3.Distance(transform.position, targetPos) <= 0.1f && !hasInitialized) {
            hasInitialized = true;
            StartCoroutine(LightingStrike());
        }
    }

    void OnDeath() {
        Destroy(gameObject);
    }
    
    IEnumerator LightingStrike() {
        yield return new WaitForSeconds(2f);
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}