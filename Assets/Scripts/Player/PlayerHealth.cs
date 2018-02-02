using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Timo Heijne
/// <summary>
/// Keep track of Cupheads health since he uses another health method than the enemy... Cuphead uses hearts the enemies use normal hp.
/// </summary>
public class PlayerHealth : MonoBehaviour {
    private float _lastDamage;
    [SerializeField] private float _minCooldown = 5;

    [Tooltip("This sets the current health of cuphead")] [SerializeField]
    private int health = 3;

    public delegate void Died();

    public Died HasDied;

    [SerializeField] private Image uiHealthImage;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite oneHP;
    [SerializeField] private Sprite twoHP;
    [SerializeField] private Sprite threeHP;

    // Use this for initialization
    void Start() {
        _lastDamage = Time.time;
    }

    public void TakeDamage() {
        if (Time.time - _lastDamage < _minCooldown) return;

        _lastDamage = Time.time;
        health -= 1;
        StartCoroutine(Blink());

        switch (health) {
            case 3:
                uiHealthImage.sprite = threeHP;
                break;
            case 2:
                uiHealthImage.sprite = twoHP;
                break;
            case 1:
                uiHealthImage.sprite = oneHP;
                break;
            default:
                uiHealthImage.sprite = null;
                break;
        }

        if (health <= 0) {
            if (HasDied != null) {
                HasDied();
            }
        }
    }

    private IEnumerator Blink() {
        Color c = _spriteRenderer.color;
        _spriteRenderer.color = new Color(0.8f, c.g, c.b, c.a);
        yield return new WaitForSeconds(0.05f);
        _spriteRenderer.color = c;
    }

    private void OnParticleCollision(GameObject other) { // This is for the paper & key attack 'n shit
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Spike") || other.CompareTag("lightningStrike") || other.CompareTag("Paper Hit") || other.CompareTag("Arrow")) {
            TakeDamage();
        }
    }
}