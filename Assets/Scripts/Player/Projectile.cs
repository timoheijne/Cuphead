using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	
	[SerializeField]
	private float _speed = 10f;

	public static ProjectileHit OnProjectileHit;

	public delegate void ProjectileHit(Vector3 hit);

	public Vector3 direction;
		
	void Update ()
	{
		transform.position += transform.right * _speed * Time.deltaTime;
	}

	private void OnDestroy()
	{
		if (OnProjectileHit != null) OnProjectileHit(transform.position);
	}
}
