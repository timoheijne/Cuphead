using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;
	
	void Awake ()
	{
		StartCoroutine(DestroyAtEnd());

		if (_animator != null) return;
		_animator = GetComponent<Animator>();
		if (_animator != null) return;
		_animator = GetComponentInChildren<Animator>();
	}

	private IEnumerator DestroyAtEnd()
	{
		float seconds = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime
		                + _animator.GetCurrentAnimatorStateInfo(0).length;
		
		yield return new WaitForSeconds(seconds);
		Destroy(gameObject, 0);
	}
}
