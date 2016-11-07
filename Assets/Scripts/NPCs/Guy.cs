﻿using UnityEngine;
using System.Collections;

public class Guy : Npc
{

    private WayPoint way;

    public override bool CanSeePlayer
    {
        get { return canSeePlayer; }
        set
        {
            if (value)
            {
                way.Stop();
            }
            else if (value != CanSeePlayer)
            {
                way.Repeat();
            }

            canSeePlayer = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        way = GetComponent<WayPoint>();
    }
}
