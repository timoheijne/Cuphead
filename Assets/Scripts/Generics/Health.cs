using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour {

	[SerializeField]
	private float _health = 100;
	private SpriteRenderer sprite;
	
	public float CurHealth {
		get { return _health; }
		set { _health = value; HazDedQuestionMark(); }
	}

	private bool deaded = false;
	
	public delegate void Died();
	public Died HasDied;

	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
	}
	
	private void HazDedQuestionMark()
	{
		// Purrfect naeming roight?
		if (!(_health <= 0)) return;
		if (HasDied == null || deaded) return;
		HasDied();
		deaded = true;
	}

	private void Damage()
	{
		StartCoroutine(Blink());
		CurHealth -= 5;
	}

	private IEnumerator Blink()
	{
		Color c = sprite.color;
		sprite.color = new Color(0.8f, c.g, c.b, c.a);
		yield return new WaitForSeconds(0.05f);
		sprite.color = c;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("Projectile"))
		{
			Damage();
			Destroy(other.gameObject);
		}
	}
}
