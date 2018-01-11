using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)),
RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private LayerMask jumpLayer;
	
	private Rigidbody2D _rigid;
	private PlayerInput _pi;

	private const float MaxSpeed = 10f;
	private const float Accel = 20f;
	private const float Deaccel = 35f;
	private const float Friction = 60f;
	private const float JumpPower = 10f;

	private float _xVelocity;
	
	void Start ()
	{
		_rigid = GetComponent<Rigidbody2D>();
		_pi = GetComponent<PlayerInput>();
	}
	
	void Update ()
	{
		Movement();
		if (_pi.Jump && CanJump) Jump();
	}

	void Jump()
	{
		var vel = _rigid.velocity;
		vel.y = JumpPower;
		_rigid.velocity = vel;
	}

	private bool CanJump
	{
		get
		{
			Vector3[] rayorigins = new Vector3[]
			{
				transform.position + new Vector3(-0.5f, -0.5f),
				transform.position + new Vector3(0.5f, -0.5f)
			};

			return rayorigins.Any(rayorigin => Physics2D.Raycast(rayorigin, Vector3.down, 0.2f, jumpLayer));
		}
	}

	void Movement()
	{
		var velocity = _rigid.velocity;
		int sign = _pi.MoveDirection;
		_xVelocity = HorizontalMove(sign, _xVelocity);
		velocity.x = _xVelocity;
		_rigid.velocity = velocity;
	}

	float HorizontalMove(int sign, float xvel)
	{
		float absxvel = Mathf.Abs(xvel);
		if (sign != 0)
		{
			float d = (int) Mathf.Sign(xvel) != sign && CanJump ? Deaccel : 0;
			xvel += (Accel * sign + d*sign) * Time.deltaTime;
			if (Mathf.Abs(xvel) > MaxSpeed) xvel = Mathf.Sign(xvel) * MaxSpeed;
			return xvel;
		}

		absxvel -= Friction * Time.deltaTime;
		if (absxvel < 0) absxvel = 0;
		return absxvel * Mathf.Sign(xvel);
	}
}