using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

	public delegate void ShootDelegate(Vector3 position);
	public static ShootDelegate OnShoot;
	public static bool limitedAmmoMode = false;
	
	[SerializeField]
	private GameObject _projectile;

	public static GameObject _muzzleFlashParticle;

	[SerializeField] private Transform _exitpoint;
	private PlayerInput _playerInput;

	private float _lastShooTime;
	public static float SHOOT_TIME = 0.23f; // shoot every SHOOT_TIME second.
	
	public bool shooting { get; private set; }

	private void Start()
	{
		_playerInput = GetComponent<PlayerInput>();
		if(!_muzzleFlashParticle) _muzzleFlashParticle = Resources.Load<GameObject>("shootparticle");

		OnShoot += (e) => { Instantiate(_muzzleFlashParticle, _exitpoint.position, Quaternion.identity); };
	}

	public bool CanShoot
	{
		get { return Time.time > _lastShooTime + SHOOT_TIME;  }
	}
	
	private void Update ()
	{
		if (CanShoot) shooting = false;
		
		if (!CanShoot || !_playerInput.Shoot) return;

		if(!limitedAmmoMode) Shoot();
	}

	public void Shoot()
	{
		int m = _playerInput.MoveDirection;

		Vector3 rotation = new Vector3();

		if (_playerInput.AimingUp)
		{
			rotation.z = m == 0 ? 90 : m == -1 ? 135 : 45;
		}
		else
		{
			rotation.z = _playerInput.LastFacingDirection == 1 ? 0 : 180;
		}
			
		GameObject _bullet = Instantiate(_projectile, _exitpoint.position, Quaternion.Euler(rotation));
		Destroy(_bullet, 5);
		

		_lastShooTime = Time.time;

		if (OnShoot != null) OnShoot(_exitpoint.transform.position);

		shooting = true;
	}
}
