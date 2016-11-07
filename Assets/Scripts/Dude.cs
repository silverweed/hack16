using UnityEngine;
using System.Collections.Generic;

public class Dude : MonoBehaviour
{
    public Vector2 directionEnemy;
    public enum Direction { South, North, West, East}
    public bool isViewPlayer { get; set; }

    [Range(0, 5)]
    public float timeWait;
    public Direction[] directions;

    private int index;
    private Dictionary<Direction, Vector2> dictonaryVector;

    void Awake()
    {
        index = 0;
        isViewPlayer = false;

        dictonaryVector = new Dictionary<Direction, Vector2>();
        dictonaryVector.Add(Direction.South, Vector2.down);
        dictonaryVector.Add(Direction.North, Vector2.up);
        dictonaryVector.Add(Direction.East, Vector2.left);
        dictonaryVector.Add(Direction.West, Vector2.right);

        Invoke("Turn", timeWait);

    }

    public void Turn()
    {
        if (!isViewPlayer)
        {
            directionEnemy = dictonaryVector[directions[index++]];
            if (index == directions.Length)
            {
                index = 0;
            }
        }

        Invoke("Turn", timeWait);
    }
}
