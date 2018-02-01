using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

// Created By Timo Heijne
[RequireComponent(typeof(BossAnimator))]
public class Boss : MonoBehaviour {

    public LazerEyes[] laserEyes;
    public GameObject thunderStrike;
    public GameObject angel;
    
    public static GameObject _player;
    private Health _health;
    private BossAnimator _bossAnimator;

    private float _attackTimer = 5;

    public enum BossState {
        Idle,
        Thunder,
        Laser,
        Dead,
        Angel
    }

    public BossState state = BossState.Idle;

    private void Start()
    {
        if(!_player)
            _player = GameObject.FindGameObjectWithTag("Player");
        _health = gameObject.AddComponent<Health>();
        _health.HasDied += HasDied;

        _bossAnimator = GetComponent<BossAnimator>();
    }

    private void HasDied()
    {
        // Send message to boss handler
        throw new NotImplementedException("WHAT THE FUCK!!!!!!!!!!!!!!!!!!! IMPOSSIBRUUUUUUUU"); 
    }

    private void Update() {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0 && state == BossState.Idle) {
            int attackType = UnityEngine.Random.Range(0, 3);

            _attackTimer = 5f;

            switch (attackType) {
                case 0:
                    state = BossState.Laser;
                    break;
                case 1:
                    state = BossState.Thunder;
                    break;
                case 2:
                    state = BossState.Angel;
                    AngelAttack();
                    break;
                default:
                    throw new IndexOutOfRangeException("Attack Type is out of range (0-2)");
            }
            
            _bossAnimator.SetState(state);
        }        
    }

    private void LazerEyes() {
        foreach (var le in laserEyes) {
            le.StartLaser();
        }

        state = BossState.Idle;
    }

    private void ThunderStrike() {
        Vector3 pos = _player.transform.position;
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        pos.y = p.y;
        GameObject go = Instantiate(thunderStrike, pos, Quaternion.identity);
        
        state = BossState.Idle;
    }

    private void AngelAttack() {
        Vector3 pos = Camera.main.transform.position;
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));
        pos.x = p.x + 5;
        pos.z = 0;
        
        GameObject go = Instantiate(angel, pos, Quaternion.identity);
        
        state = BossState.Idle;
    }
}
