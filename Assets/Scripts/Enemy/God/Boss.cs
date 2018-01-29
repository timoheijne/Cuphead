using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

// Created By Timo Heijne
public class Boss : MonoBehaviour {

    public LazerEyes[] laserEyes;
    public GameObject thunderStrike;
    public GameObject angel;
    
    public static GameObject _player;
    private Health _health;

    private float _attackTimer = 5;

    private void Start()
    {
        if(!_player)
            _player = GameObject.FindGameObjectWithTag("Player");
        _health = gameObject.AddComponent<Health>();
        _health.HasDied += HasDied;
    }

    private void HasDied()
    {
        throw new NotImplementedException("WHAT THE FUCK!!!!!!!!!!!!!!!!!!! IMPOSSIBRUUUUUUUU"); // since its the final boss player has won
    }

    private void Update() {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0) {
            int attackType = UnityEngine.Random.Range(0, 3);
            ThunderStrike();
            AngelAttack();
            
            _attackTimer = 5f;
        }  

        /*switch(attackType)
        {
            case 0:
                LazerEyes();
                break;
            case 1:
                ThunderStrike();
                break;               
            case 2:
                AngelAttack();
                break;
            default:
                throw new IndexOutOfRangeException("Attack Type is out of range (0-2)");
        }*/
        
        if (Input.GetKeyDown(KeyCode.L)) {
            UnityEngine.Debug.Log("Absolutely Worthless");
            foreach (var le in laserEyes) {
                StartCoroutine(le.RunLaser());
            } 
        }	

        
    }

    private void LazerEyes() {
        foreach (var le in laserEyes) {
            le.StartLaser();
        }
    }

    private void ThunderStrike() {
        Vector3 pos = _player.transform.position;
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        pos.y = p.y;
        GameObject go = Instantiate(thunderStrike, pos, Quaternion.identity);
    }

    private void AngelAttack() {
        Vector3 pos = Camera.main.transform.position;
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));
        pos.x = p.x + 5;
        pos.z = 0;
        GameObject go = Instantiate(angel, pos, Quaternion.identity);
    }
}
