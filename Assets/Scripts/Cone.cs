using UnityEngine;
using System.Collections;
using System;

public class Cone : MonoBehaviour
{
    public float angleCone;
    public float distanceCone;
    public LayerMask maskObstacle;

    private Dude I;

    // Use this for initialization
    void Awake ()
    {
        I = GetComponent<Dude>();
	
	}

    // Update is called once per frame
    void Update()
    {
        Vector2 auxDifference = MovePlayer.player.position - transform.position;
        Vector2 directionEnemy = GetComponent<Dude>().directionEnemy;

        //Il personaggio è nel cono visuale?
        I.isViewPlayer = false;
        if (Vector3.Angle(directionEnemy, auxDifference) < angleCone / 2 && auxDifference.magnitude < distanceCone)
        {

            float dist = Vector2.Distance(MovePlayer.player.position, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, auxDifference.normalized, dist, maskObstacle);

            //tra personaggio e il soldato ci sono ostacoli?
            if (hit.collider == null)
            {
                Debug.Log("preso");
            }

            I.isViewPlayer = !Convert.ToBoolean(hit.collider);
        }
    }
}
