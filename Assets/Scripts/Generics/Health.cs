using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// Created By Timo Heijne
[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour {
    [Tooltip("Set the health of this object. This does not indicate max health but current health.. There is no max")]
    [SerializeField]
    private float _health = 100;

    public static float DamageTaken = 1;

    private SpriteRenderer sprite;
    private Color defaultSpriteColor;

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
        defaultSpriteColor = sprite.color;
    }

    private void HazDedQuestionMark() {
        // Purrfect naeming roight?
        if (CurHealth > 0) return;
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
        sprite.color = new Color(0.8f, 0.6f, 0.6f, 1f);
        yield return new WaitForSeconds(0.05f);
        sprite.color = defaultSpriteColor;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Projectile") {
            Damage();
            EffectManager.instance.SpawnHitAtPoint(other.transform.position);
            Destroy(other.gameObject);
        }
    }
}
