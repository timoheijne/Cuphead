using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour 
{
    public int MoveDirection
    {
        get
        {
            return Input.GetKey(KeyCode.RightArrow) ? 1 
                : Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;
        }
    }

    public bool AimingUp
    {
        get { return Input.GetKey(KeyCode.UpArrow); }
    }

    public int LastFacingDirection { get; private set; }

    void Start()
    {
        LastFacingDirection = 1;
    }
   
    void Update()
    {
        if (MoveDirection != 0) LastFacingDirection = MoveDirection;
    }
    
    public bool Jump
    {
        get { return Input.GetKeyDown(KeyCode.Space); }
    }
}
