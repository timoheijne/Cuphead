using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour 
{
    public int MoveDirection
    {
        get
        {
            return Input.GetKey(KeyCode.D) ? 1 
                : Input.GetKey(KeyCode.A) ? -1 : 0;
        }
    }

    public bool Jump
    {
        get { return Input.GetKeyDown(KeyCode.Space); }
    }
}
