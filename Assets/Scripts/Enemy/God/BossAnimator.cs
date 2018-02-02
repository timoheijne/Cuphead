using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Created By Timo Heijne
[RequireComponent(typeof(Animator))]
public class BossAnimator : MonoBehaviour {
    private Animator _animator;

    // Use this for initialization
    void Start() {
        _animator = GetComponent<Animator>();
    }

    public void SetState(Boss.BossState state) {
        switch (state) {
            case Boss.BossState.Idle:
                break;
            case Boss.BossState.Thunder:
                _animator.SetTrigger("Throw");
                break;
            case Boss.BossState.Laser:
                _animator.SetTrigger("Laser");
                break;
            case Boss.BossState.Dead:
                _animator.SetBool("Dead", true);
                break;
            case Boss.BossState.Angel:
                break;
            default:
                throw new ArgumentOutOfRangeException("state", state, null);
        }
    }
}