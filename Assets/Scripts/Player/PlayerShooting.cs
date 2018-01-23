using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	[SerializeField]
	private GameObject _projectile;
	private PlayerInput _playerInput;

	private float _lastShooTime;
	private const float SHOOT_TIME = 0.23f; // shoot every SHOOT_TIME second.

	void Start()
	{
		_playerInput = GetComponent<PlayerInput>();
	}
	
	void Update () {
		if (!(Time.time > _lastShooTime + SHOOT_TIME) || !_playerInput.Shoot) return;
		
		int m = _playerInput.MoveDirection;

		Vector3 rotation = new Vector3();

		if (_playerInput.AimingUp)
		{
			rotation.z = m == 0 ? 90 : m == -1 ? 135 : 45;
		}
		else
		{
			rotation.z = _playerInput.LastFacingDirection == 1 ? 0 : 180;
			print(_playerInput.LastFacingDirection);
		}
			
		GameObject _bullet = Instantiate(_projectile, transform.position, Quaternion.Euler(rotation));
		Destroy(_bullet, 5);

		_lastShooTime = Time.time;
	}
}
