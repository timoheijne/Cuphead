using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	[SerializeField]
	private float _health = 100;
	
	public float CurHealth {
		get { return _health; }
		set { _health = value; HazDedQuestionMark(); }
	}
	
	delegate void Died();
	Died HasDied;

	
	
	private void HazDedQuestionMark() { // Purrfect naeming roight?
		if (_health <= 0) {
			if (HasDied != null)
				HasDied();
		}
	}
	
}
