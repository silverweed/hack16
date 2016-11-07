using UnityEngine;
using System.Collections.Generic;

public class Dude : Npc
{
    [Range(0, 5)]
    public float timeWait;
    public Direction[] directions;

    private int index;
    private Dictionary<Direction, Vector2> dictonaryVector;

    void Awake()
    {
        index = 0;
        CanSeePlayer = false;

        dictonaryVector = new Dictionary<Direction, Vector2>();
        dictonaryVector.Add(Direction.South, Vector2.down);
        dictonaryVector.Add(Direction.North, Vector2.up);
        dictonaryVector.Add(Direction.East, Vector2.left);
        dictonaryVector.Add(Direction.West, Vector2.right);

        Invoke("Turn", timeWait);

    }

    public void Turn()
    {
        if (!CanSeePlayer)
        {
            direction = dictonaryVector[directions[index++]];
            if (index == directions.Length)
            {
                index = 0;
            }
        }

        Invoke("Turn", timeWait);
    }
}
