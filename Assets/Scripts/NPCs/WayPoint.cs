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
    private bool inverse;

    [Range(0, 5)]
    public float timeWait;

    private int index;

    // Use this for initialization
    void Awake ()
    {
        transformWayPoint = new Transform[transform.parent.GetChild(1).childCount];
        npc = GetComponent<Npc>();
        index = 0;
        wait = true;
        inverse = false;

        foreach (Transform child in transform.parent.GetChild(1))
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

    private void CreateDirection()
    {
        if (!inverse)
        {
            directionWay = (transformWayPoint[index + 1].position - transformWayPoint[index].position).normalized;
        }
        else
        {
            directionWay = (transformWayPoint[index - 1].position - transformWayPoint[index].position).normalized;
        }
    }

    private void Go()
    {
        CreateDirection();
        wait = false;
    }
	
	// Update is called once per frame
	private void Update ()
    {
        npc.direction = directionWay;

        if (!wait)
        {
            transform.Translate(directionWay * speed);

            if (!inverse)
            {
                Up();
            }
            else
            {
                Down();
            }
        }
	}

    private void Up()
    {
        if (transformWayPoint[index + 1].GetComponent<BoxCollider2D>().bounds.Contains(transform.position))
        {
            index++;

            if (index == transformWayPoint.Length - 1)
            {
                inverse = true;
            }

            wait = true;
            Invoke("Go", timeWait);
        }
    }

    private void Down()
    {
        if (transformWayPoint[index - 1].GetComponent<BoxCollider2D>().bounds.Contains(transform.position))
        {
            index--;

            if (index == 0)
            {
                inverse = false;
            }

            wait = true;
            Invoke("Go", timeWait);
        }
    }
}
