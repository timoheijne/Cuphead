using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	
	[SerializeField]
	private float _speed = 10f;

	private float lifetime = 0;

	public static ProjectileHit OnProjectileHit;

	public delegate void ProjectileHit(Vector3 hit);

	public Vector3 direction;
	private bool execute = true;
		
	void Update ()
	{
		transform.position += transform.right * _speed * Time.deltaTime;

		// I'm sorry, this is a hack so that the destruction sound does not play if the object is destroyed...

		lifetime += Time.deltaTime;
		if (lifetime > 4f) execute = false;
		
		
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.E))
			Destroy(gameObject, 0.1f);
#endif
	}

	private void OnDestroy()
	{
		if (OnProjectileHit != null && execute) OnProjectileHit(transform.position);
	}
}
