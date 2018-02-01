using System.Collections;
using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class Slotmachine : MonoBehaviour
{
	public EventHandler ALlMovedIn;

	[SerializeField] private Canvas _canvas;
	[SerializeField] private Text _modifierText;
	[SerializeField] private GameObject[] _icons;
	private Animator _animator;
	private Randomiser _currentModifier;
	private bool rolling = false;
	private bool showText = false;
	

	private void Start()
	{
		_animator = GetComponent<Animator>();
		_canvas.worldCamera = Camera.main;
	}

	public void StartSlotmachine(Sprite sprite, string Text, Randomiser mod)
	{
		Time.timeScale = 0;
		_currentModifier = mod;

		foreach (var icon in _icons)
		{
			icon.GetComponent<SpriteRenderer>().sprite = sprite;
			icon.SetActive(false);
			_modifierText.text = Text;
		}
	}

	void Update()
	{
		if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Space) && !rolling)
			MoveInAllIcons();

		if (showText)
		{
			_modifierText.rectTransform.localScale =
				Vector3.Lerp(_modifierText.rectTransform.localScale, 
					new Vector3(1, 1, 1), 8 * Time.unscaledDeltaTime);
			_modifierText.rectTransform.rotation =
				Quaternion.Lerp(_modifierText.rectTransform.rotation,
					Quaternion.Euler(0, 0, 16), 8 * Time.unscaledDeltaTime);
		}
		else
		{
			_modifierText.gameObject.SetActive(false);
		}
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
			yield return new WaitForSecondsRealtime(1.2f);
			yield return StartCoroutine(MoveGameObjectIn(t));
		}
		
		// show text
		showText = true;
		_modifierText.rectTransform.localScale =
			new Vector3(0, 0, 0);
		_modifierText.rectTransform.eulerAngles =
			new Vector3(0,0,36);

		_canvas.gameObject.SetActive(true);
		_modifierText.gameObject.SetActive(true);

		yield return new WaitForSecondsRealtime(2.0f);

		showText = false;
		_canvas.gameObject.SetActive(true);
		_modifierText.gameObject.SetActive(true);
		
		Time.timeScale = 1;
		gameObject.SetActive(false);
		_currentModifier.StartMod();
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
