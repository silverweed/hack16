using UnityEngine;
using System.Collections;

public class SuperGuy : Npc
{
    [Range(0, 5)]
    public float distance;

    [Range(0, 1)]
    public float speeed;

    private bool find;
    private Transform player;
    private WayPoint way;

    public override bool CanSeePlayer
    {
        get { return canSeePlayer; }
        set
        {
            if (value)
            {
                way.Stop();
                find = true;
            }
            else if (value != CanSeePlayer)
            {
                find = false;
                way.Return();
            }

            canSeePlayer = value;
        }
    }

    private void Update()
    {
        if (find)
        {
            Vector2 dir = (player.position - this.transform.position);
            if (dir.magnitude > distance)
            {
                direction = dir.normalized;
                transform.Translate(dir.normalized * speeed);
            }
        }

    }


    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        find = false;
        way = GetComponent<WayPoint>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

}
