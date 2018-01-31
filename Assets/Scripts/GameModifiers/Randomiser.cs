using System.Collections;
using System.Collections.Generic;
using GameModifiers.Modifiers;
using UnityEngine;

public class Randomiser : MonoBehaviour
{
	private Modifier _currentModifier;

	private List<Modifier> AllModifiers;
	private List<Sprite> ModifierSprites;

	private GameObject _player;

	public GameObject PLAYER
	{
		get { return _player ?? (_player = GameObject.FindGameObjectWithTag("Player")); }
	}
	

	void Start()
	{
		FillModifiers();
		RandomiseMod();
	}
	
	void FillModifiers()
	{
		AllModifiers = new List<Modifier>()
		{
			new CameraRotationModifier("CONSTANTLY ROTATING CAMERA"),
			new InvertedControlsModifier("INVERTED CONTROLS"),
			new JumpDelayModifier("JUMP DELAY"),
			new CoolModeModifier("COOL MODE"),
			new LimitedAmmoModifier("LIMITED AMMO")
		};

		ModifierSprites = new List<Sprite>()
		{
			Resources.Load<Sprite>("draaien beeld"),
			Resources.Load<Sprite>("inverted controls"),
			Resources.Load<Sprite>("jump delay"),
			Resources.Load<Sprite>("cool mode"),
			Resources.Load<Sprite>("limited ammo")
		};
	}
	
	void RandomiseMod()
	{
		if(_currentModifier != null) _currentModifier.DestroyMod(this);
		_currentModifier = GetRandomModifier();
		_currentModifier.StartMod(this);
	}

	void Update()
	{
		if(_currentModifier != null) _currentModifier.UpdateMod(this);
		
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Q))
		{
			RandomiseMod();
		}
		
		
#endif
	}
	
	private Modifier GetRandomModifier()
	{
		if (AllModifiers.Count == 0) return null;
		return AllModifiers[Random.Range(0,AllModifiers.Count)];
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10,10,200,200), _currentModifier.GetType().Name);
	}
}
