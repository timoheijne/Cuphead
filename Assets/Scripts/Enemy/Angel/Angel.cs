using System;
using System.Collections;
using UnityEngine;

// Created By Timo Heijne
/// <summary>
/// Keep track of an angel which is spawned by the boss
/// </summary>
public class Angel : MonoBehaviour {
    private Camera _main;

    public float speed = 1.0F;
    private float _startTime;
    private float _journeyLength;

    private Vector3 _offset;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    [SerializeField] private GameObject _arrow;

    [SerializeField] private Vector3 _angelOffset;

    enum AngelStatus {
        MovingIn,
        Arrow,
        MovingOut
    }

    private AngelStatus _status = AngelStatus.MovingIn;

    private void Start() {
        transform.position += _angelOffset;
        _animator = GetComponent<Animator>();
        _main = Camera.main;
        _offset = _main.transform.position - transform.position;
        _offset.z = 0;
        _startTime = Time.time;

        _startPosition = transform.position;
        _endPosition = _startPosition;
        _endPosition.x += 2;
        _endPosition.z = 0;

        _journeyLength = Vector3.Distance(_startPosition, _endPosition);
        _spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(HandleState());
    }

    void MovingIn() { // TODO: Refactor This Lerp Into One Function For Both Moving in/out
        Vector3 pos = Camera.main.transform.position;
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));
        pos.x = p.x - 1.5f;
        pos.z = 0;
        pos += _angelOffset;

        float distCovered = (Time.time - _startTime) * speed;
        float fracJourney = distCovered / _journeyLength;
        Vector3 endPos = Vector3.Lerp(_startPosition, pos, fracJourney);
        transform.position = endPos;

        if (Vector3.Distance(transform.position, pos) <= 0.1f) {
            _status = AngelStatus.Arrow;
            _animator.SetTrigger("Shoot");
        }
    }

    void MovingOut() { // TODO: Refactor This Lerp Into One Function For Both Moving in/out
        _spriteRenderer.flipX = true;
        Vector3 pos = Camera.main.transform.position;
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));
        pos.x = p.x + 1.5f;
        pos.z = 0;
        pos += _angelOffset;

        float distCovered = (Time.time - _startTime) * speed;
        float fracJourney = distCovered / _journeyLength;
        Vector3 endPos = Vector3.Lerp(_startPosition, pos, fracJourney);

        transform.position = endPos;

        if (Vector3.Distance(transform.position, pos) <= 0.1f) {
            Destroy(gameObject);
        }
    }

    void ShootArrow() {
        Transform go = Instantiate(_arrow, transform.position, Quaternion.identity).transform;
        go.transform.LookAt(Boss.player.transform.position);

        _status = AngelStatus.MovingOut;
        _startTime = Time.time;
        _startPosition = transform.position;
    }

    IEnumerator HandleState() {
        AngelStatus lastStatus = _status;
        while (true) {
            if (_status != lastStatus) {
                yield return new WaitForSeconds(2f);
                lastStatus = _status;

                if (lastStatus == AngelStatus.MovingOut) {
                    _startTime = Time.time;
                    _startPosition = transform.position;
                }
            }

            switch (_status) {
                case AngelStatus.MovingIn:
                    MovingIn();
                    break;
                case AngelStatus.Arrow:
                    //ShootArrow(); -- We handle this switch with an animation event
                    break;
                case AngelStatus.MovingOut:
                    MovingOut();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            yield return new WaitForEndOfFrame();
        }
    }


    float ClampAngle(float angle, float from, float to) {
        if (angle > 180) angle = 360 - angle;
        angle = Mathf.Clamp(angle, from, to);
        if (angle < 0) angle = 360 + angle;

        return angle;
    }
}