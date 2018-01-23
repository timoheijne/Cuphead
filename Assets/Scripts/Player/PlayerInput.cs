using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static bool inverted;
    public static bool jumpdelay;
    
    public int MoveDirection
    {
        get
        {   
            return (Input.GetKey(KeyCode.RightArrow) ? 1 
                : Input.GetKey(KeyCode.LeftArrow) ? -1 : 0) * (inverted ? -1 : 1);
        }
    }

    public bool AimingUp
    {
        get
        {
            var k = inverted ? KeyCode.DownArrow : KeyCode.UpArrow;
            return Input.GetKey(k); 
        }
    }

    public int LastFacingDirection { get; private set; }

    private void Start()
    {
        LastFacingDirection = 1;
    }
   
    private void Update()
    {
        if (MoveDirection != 0) LastFacingDirection = MoveDirection;
    }

    public bool Shoot
    {
        get { return Input.GetKey(KeyCode.X); }
    }
    
    public bool Jump
    {
        get { return Input.GetKeyDown(KeyCode.Z); }
    }
}
