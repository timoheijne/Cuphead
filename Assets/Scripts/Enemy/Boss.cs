using System;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private static GameObject player;
    private Health health;
    private int stage = 3;

    private void Start()
    {
        if(!player)
            player = GameObject.FindGameObjectWithTag("Player");
        health = gameObject.AddComponent<Health>();
        health.HasDied += HasDied;
    }

    private void HasDied()
    {
        throw new NotImplementedException("WHAT THE FUCK!!!!!!!!!!!!!!!!!!!");
    }

    private void Update()
    {
        switch (stage)
        {
                case 3:
                    ThirdStage();
                    break;
                default: throw new OutOfMemoryException("WHAT THE FUCK!!!!!");
        }
    }

    private void ThirdStage()
    {
        
    }
}
