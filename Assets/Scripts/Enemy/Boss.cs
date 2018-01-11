using System;
using Enemy;
using UnityEngine;

public class Boss : MonoBehaviour, IHurtable
{
    private static GameObject player;
    
    private int stage = 3;
    private int hp = 1000;

    private void Start()
    {
        if(!player)
            player = GameObject.FindGameObjectWithTag("Player");
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
