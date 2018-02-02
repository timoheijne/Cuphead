using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using GameModifiers.Modifiers;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// UNG UNG ME DEVELOPER AAAAAA!!!!!!!
/// fuck
/// </summary>
public class Randomiser : MonoBehaviour
{
	private Modifier _currentModifier;
	private Sprite _currentSprite;

	[SerializeField]
	private List<Modifier> AllModifiers;
	[SerializeField]
	private List<Sprite> ModifierSprites;

	private GameObject _slotMachine; // prefab
	
	private Slotmachine _randomiser;
	private GameObject _player;
	
	public Sprite CurrentSprite
	{
		get { return _currentSprite; }
	}

	public GameObject PLAYER
	{
		get { return _player ?? (_player = GameObject.FindGameObjectWithTag("Player")); }
	}

	private bool update = false;

	void Awake()
	{
		FillModifiers();
		DontDestroyOnLoad(gameObject);
		SceneManager.sceneLoaded += SceneLoaded;
		MenuButtons.CrazyMode = true;
	}

	private void SceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
	{
		if (arg0.buildIndex != 1) return;

		_slotMachine = Resources.Load<GameObject>("slotmachine");
		_randomiser = Instantiate(_slotMachine, new Vector3(0, 0, -8), Quaternion.identity)
			.GetComponent<Slotmachine>();
		
		RollSlotMachine();
	}

	void RollSlotMachine()
	{
		int rnumber = GetRandomNumber();
		RandomiseMod(GetRandomModifier(rnumber));
		_currentSprite = ModifierSprites[rnumber];
		_randomiser.StartSlotmachine(_currentSprite, _currentModifier.Name, this);
		Time.timeScale = 0;
	}

	void FillModifiers()
	{
		AllModifiers = new List<Modifier>()
		{
			new CameraRotationModifier("CONSTANTLY ROTATING CAMERA"),
			new InvertedControlsModifier("INVERTED CONTROLS"),
			new JumpDelayModifier("JUMP DELAY"),
			new CoolModeModifier("COOL MODE"),
			new LimitedAmmoModifier("LIMITED AMMO"),
			new LowRangeModifier("LOW RANGE"),
			new MoreDamageModifier("EXTRA DAMAGE!"),
			new FasterBulletsModifier("QUICK BULLETS"),
		};

		ModifierSprites = new List<Sprite>()
		{
			Resources.Load<Sprite>("draaien beeld"),
			Resources.Load<Sprite>("inverted controls"),
			Resources.Load<Sprite>("jump delay"),
			Resources.Load<Sprite>("cool mode"),
			Resources.Load<Sprite>("limited bullets"),
			Resources.Load<Sprite>("kleine range"),
			Resources.Load<Sprite>("meer damga"),
			Resources.Load<Sprite>("snelle kogels"),

		};
	}
	
	void RandomiseMod(Modifier mod)
	{
		if(_currentModifier != null) _currentModifier.DestroyMod(this);
		_currentModifier = mod;
		update = false;
	}

	public void StartMod()
	{
		_currentModifier.InitMod(this);
		update = true;
	}

	void Update()
	{
		if(_currentModifier != null && update) _currentModifier.UpdateMod(this);
		
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Q))
		{
			RandomiseMod(GetRandomModifier(GetRandomNumber()));
			_currentModifier.StartMod(this);
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(
				UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		}
#endif
	}

	private int GetRandomNumber()
	{
		return Random.Range(0, AllModifiers.Count);
	}
	
	private Modifier GetRandomModifier(int r)
	{
		if (AllModifiers.Count == 0) return null;
		return AllModifiers[r];
	}

#if UNITY_EDITOR
	private void OnGUI()
	{
		if(_currentModifier != null)
			GUI.Label(new Rect(10,10,200,200), _currentModifier.GetType().Name);
	}
#endif
}
