using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	
	[SerializeField]
	private float _speed = 5;

	public Vector3 direction;
		
	void Update ()
	{
		transform.position += transform.right * _speed * Time.deltaTime;
	}
}
