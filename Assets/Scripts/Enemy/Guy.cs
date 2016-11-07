using UnityEngine;
using System.Collections;

public class Guy : Npc
{

    private Vector2[] positionWayPoint;
    private Vector2 directionWay;
    private bool wait;

    [Range(0, 5)]
    public float timeWait;

    private int index;

    // Use this for initialization
    void Awake ()
    {
        positionWayPoint = new Vector2[transform.parent.GetChild(1).childCount];
        index = 0;
        wait = true;

        foreach (Transform child in transform.parent.GetChild(1))
        {
            positionWayPoint[index] = child.position;
            index++;
        }

        index = 0;
        Invoke("Go", timeWait);
	
	}

    private void Go()
    {
        wait = false;
        directionWay = (positionWayPoint[index + 1] - positionWayPoint[index]).normalized;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!wait)
        {
            transform.Translate(directionWay);
        }
	}
}
