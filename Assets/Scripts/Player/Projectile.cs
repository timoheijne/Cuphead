using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	
	public static float Speed = 10f;
	public static bool shortLifeSpan = false;

	private float lifetime = 0;

	public static ProjectileHit OnProjectileHit;

	public delegate void ProjectileHit(Vector3 hit);

	public Vector3 direction;
	private bool execute = true;
		
	void Update ()
	{
		transform.position += transform.right * Speed * Time.deltaTime;

		// I'm sorry, this is a hack so that the destruction sound does not play if the object is destroyed...

		lifetime += Time.deltaTime;
		if (lifetime > 4f && !shortLifeSpan) execute = false;
		else if (shortLifeSpan && lifetime > 1)
		{
			execute = false;
			Destroy(gameObject);
		}
		
		
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
