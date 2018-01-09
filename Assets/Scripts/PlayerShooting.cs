using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	[SerializeField]
	private GameObject _projectile;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = transform.position;
		direction.z = 0;

		if (Input.GetKey(KeyCode.UpArrow))
			direction += Vector3.up;

		if (Input.GetKey(KeyCode.DownArrow))
			direction += -Vector3.up;

		if (Input.GetKey(KeyCode.LeftArrow))
			direction += -Vector3.right;
		
		if (Input.GetKey(KeyCode.RightArrow))
			direction += Vector3.right;

		if (Input.GetKeyDown(KeyCode.X)) {
			// Instantiate Projectile
			print(direction);
			
			if (direction == Vector3.zero)
				direction = Vector3.right;

			GameObject _bullet = Instantiate(_projectile, transform);
			Vector3 pos = transform.position;
			//pos.z = 0;

			Quaternion rotation = Quaternion.LookRotation(pos + direction);

			if (direction.x > 0) {
				rotation.eulerAngles = new Vector3(0, 0, rotation.eulerAngles.y - rotation.eulerAngles.x);
			} else if (direction.x == 0f && direction.y != 0f) {
				rotation.eulerAngles = new Vector3(0, 0, rotation.eulerAngles.y);
			} else {
				rotation.eulerAngles = new Vector3(0, 0, 360 - (rotation.eulerAngles.y - rotation.eulerAngles.x));
			}			
			
			_bullet.transform.position = pos;
			_bullet.transform.rotation = rotation;
			_bullet.GetComponent<Projectile>().direction = direction;
			Destroy(_bullet, 5);
		}
	}
}
