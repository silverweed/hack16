using UnityEngine;
using System.Collections.Generic;

public class Dude : Npc
{
    [Range(0, 5)]
    public float timeWait;
    public Direction[] directions;

    private int index;
    

    protected override void Awake()
    {
        base.Awake();
        index = 0;
        CanSeePlayer = false;
        Invoke("Turn", timeWait);
    }

    public void Turn()
    {
        Debug.Log("Begin Turn()");
        if (!CanSeePlayer)
        {
            Debug.Log("in !CanSeePlayer Turn()");
            direction = dictonaryVector[directions[index++]];
            if (index == directions.Length)
            {
                index = 0;
            }
        }

        Invoke("Turn", timeWait);

        Debug.Log("End Turn()");
    }
}
