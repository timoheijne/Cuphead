using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created By Timo Heijne
[RequireComponent(typeof(Animator))]
public class GateAnimation : MonoBehaviour {
    private Animator _animator;

    public void Start() {
        _animator = GetComponent<Animator>();
    }

    public void SetState(EnemyGate.GateStatus type) {
        switch (type) {
            case EnemyGate.GateStatus.Spikes:
                Trigger("Spike");
                break;
            case EnemyGate.GateStatus.Trash:
                Trigger("Trash");
                break;
            case EnemyGate.GateStatus.Smash:
                Trigger("Smash");
                break;
            case EnemyGate.GateStatus.Idle:
                break;
            case EnemyGate.GateStatus.Dead:
                _animator.SetBool("isDead", true);
                break;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }
    }

    public void Trigger(string name) {
        _animator.SetTrigger(name);
    }
}