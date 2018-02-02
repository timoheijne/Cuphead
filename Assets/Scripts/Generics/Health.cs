using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne
[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour {
    [Tooltip("Set the health of this object. This does not indicate max health but current health.. There is no max")]
    [SerializeField]
    private float _health = 100;

    public static float DamageTaken = 1;

    private SpriteRenderer sprite;

    public float CurHealth {
        get { return _health; }
        set {
            _health = value;
            HazDedQuestionMark();
        }
    }

    public delegate void Died();

    public Died HasDied;
    public static EventHandler OnHit;

    [HideInInspector] public bool dead = false;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void HazDedQuestionMark() {
        // Purrfect naeming roight?
        if (_health > 0) return;
        if (HasDied == null || dead) return;
        HasDied();
        dead = true;
    }

    private void Damage() {
        StartCoroutine(Blink());
        if (OnHit != null) OnHit(this, null);
        CurHealth -= DamageTaken;
    }

    private IEnumerator Blink() {
        Color c = sprite.color;
        sprite.color = new Color(0.8f, c.g, c.b, c.a);
        yield return new WaitForSeconds(0.05f);
        sprite.color = c;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Projectile") {
            Damage();
            EffectManager.instance.SpawnHitAtPoint(other.transform.position);
            Destroy(other.gameObject);
        }
    }
}
