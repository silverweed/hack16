using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour
{
    [Range(0, 1)]
    public float speed;

    private Npc npc;
    private Transform[] transformWayPoint;
    private Vector2 directionWay;
    public bool wait { get; set; }
    private bool notInverse;

    [Range(0, 5)]
    public float timeWait;

    private int index;

    // Use this for initialization
    void Awake ()
    {
        transformWayPoint = new Transform[transform.parent.FindChild("WayPoints").childCount];
        npc = GetComponent<Npc>();
        index = 0;
        wait = true;
        notInverse = true;

        foreach (Transform child in transform.parent.FindChild("WayPoints"))
        {
            transformWayPoint[index] = child;
            index++;
        }

        index = 0;
        directionWay = (transformWayPoint[index + 1].position - transformWayPoint[index].position).normalized;
        Invoke("Go", timeWait);
	
	}

    public void Stop()
    {
        wait = true;
        CancelInvoke();
    }

    public void Repeat()
    {
        wait = false;
        CreateDirection();
        CancelInvoke();
    }

    public void Return()
    {
        wait = false;
        directionWay = (transformWayPoint[0].position - this.transform.position).normalized;
        notInverse = true;
        index = -1;
    }

    private void CreateDirection()
    {
        directionWay = (transformWayPoint[getNextIndex()].position - transform.position).normalized;
    }

    private void Go()
    {
        CreateDirection();
        wait = false;
    }
	
	// Update is called once per frame
	private void Update ()
    {
        if (!wait)
        {
            npc.direction = directionWay;
            transform.Translate(directionWay * speed);
            Up();
        }
	}

    private int getNextIndex()
    {
        if (notInverse)
        {
            return index + 1;
        }
        else
        {
            return index - 1;
        }
    }

    private void Up()
    {
        if (transformWayPoint[getNextIndex()].GetComponent<BoxCollider2D>().bounds.Contains(transform.position))
        {
            index = getNextIndex();

            if (index == transformWayPoint.Length - 1)
            {
                notInverse = false;
            }
            if (index == 0)
            {
                notInverse = true;
            }

            wait = true;
            Invoke("Go", timeWait);
        }
    }
}
