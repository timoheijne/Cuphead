using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GateAnimation : MonoBehaviour {

	private Animator _animator;

	public void Start() {
		_animator = GetComponent<Animator>();
	}

	public void SetState(EnemyGate.GateStatus type) {
		ResetAnimations();
		switch(type) {
			case EnemyGate.GateStatus.Spikes:
				_animator.SetBool("isSpiking", true);
				break;
			case EnemyGate.GateStatus.Trash:
				_animator.SetBool("isTrashing", true);
				break;
			case EnemyGate.GateStatus.Smash:
				_animator.SetBool("isSmashing", true);
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

	private void ResetAnimations() {
		foreach (var parameter in _animator.parameters) {
			if (parameter.type == AnimatorControllerParameterType.Bool) {
				_animator.SetBool(parameter.name, false);
			}
		}
	}

	
}
