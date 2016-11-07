using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Npc : MonoBehaviour
{
    public Vector2 direction;
    private bool canSeePlayer;
    protected Dictionary<Direction, Vector2> dictonaryVector;

    public bool CanSeePlayer
    {
        get { return canSeePlayer; }
        set
        {
            canSeePlayer = value;
        }
    }

    protected virtual void Awake()
    {
        dictonaryVector = new Dictionary<Direction, Vector2>();
        dictonaryVector.Add(Direction.South, Vector2.down);
        dictonaryVector.Add(Direction.North, Vector2.up);
        dictonaryVector.Add(Direction.East, Vector2.left);
        dictonaryVector.Add(Direction.West, Vector2.right);
    }
}
