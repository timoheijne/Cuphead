using System.Collections;
using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Slotmachine : MonoBehaviour
{
	public EventHandler ALlMovedIn;
	
	[SerializeField] private GameObject[] _icons;
	private Animator _animator;

	private bool rolling = false;
	

	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	public void StartSlotmachine(Sprite sprite, string Text)
	{
		Time.timeScale = 0;

		foreach (var icon in _icons)
		{
			icon.GetComponent<SpriteRenderer>().sprite = sprite;
			icon.SetActive(false);
		}
	}

	void Update()
	{
		if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Space) && !rolling)
			MoveInAllIcons();
	}
	
	void MoveInAllIcons()
	{
		rolling = true;
		StartCoroutine(StartSlotmachine());
		var audiosource = GetComponent<AudioSource>();
		audiosource.Play();
		audiosource.time = 4.0f;
	}

	IEnumerator StartSlotmachine()
	{
		_animator.SetTrigger("Start");
		
		do
		{
			yield return new WaitForEndOfFrame();
		} while (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Slotmachine - Pull"));

		foreach (var t in _icons)
		{
			yield return new WaitForSecondsRealtime(1f);
			yield return StartCoroutine(MoveGameObjectIn(t));
		}
		
		// show text
		
		Time.timeScale = 1;
		gameObject.SetActive(false);
	}

	IEnumerator MoveGameObjectIn(GameObject g)
	{
		const float wantedY = -0.155f;
		const float speed = 10;
		var wantedpos = g.transform.localPosition;
		wantedpos.y = wantedY;
		
		g.SetActive(true);

		var actualpos = g.transform.localPosition;
		actualpos.y = 0.7f;
		g.transform.localPosition = actualpos;
		for (;;)
		{
			g.transform.localPosition = Vector3.Lerp(g.transform.localPosition, wantedpos, speed * Time.unscaledDeltaTime);

			if ((g.transform.localPosition - wantedpos).magnitude < 0.02f)
			{
				g.transform.localPosition = wantedpos;
				break;
			}
			
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForEndOfFrame();
	}
}
