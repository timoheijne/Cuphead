using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class Boss : MonoBehaviour {

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
        throw new NotImplementedException("WHAT THE FUCK!!!!!!!!!!!!!!!!!!! IMPOSSIBRUUUUUUUU");
    }

    private void Update() {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer > 0) return;

        int attackType = UnityEngine.Random.Range(0, 3);
        ThunderStrike();
        AngelAttack();

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

        _attackTimer = 5f;
    }

    private void LazerEyes() {
        Vector3 lazerOrigin = _player.transform.position + (_player.transform.right * 5);
        
        
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
