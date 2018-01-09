using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	
	[SerializeField]
	private float _speed = 5;
	
	public Vector3 direction { get; set; }
		
	// Update is called once per frame
	void Update () {
		transform.position += direction * _speed * Time.deltaTime;
	}
}
