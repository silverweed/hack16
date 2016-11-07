using UnityEngine;
using System.Collections;

public class Cone : MonoBehaviour
{
    public Vector2 directionEnemy;
    public float angleCone;
    public float distanceCone;
    public LayerMask maskObstacle;

    // Use this for initialization
    void Start ()
    {
	
	}

    // Update is called once per frame
    void Update()
    {
        Vector2 auxDifference = MovePlayer.player.position - transform.position;

        //Il personaggio è nel cono visuale?
        if (Vector3.Angle(directionEnemy, auxDifference) < angleCone / 2 && auxDifference.magnitude < distanceCone)
        {

            float dist = Vector2.Distance(MovePlayer.player.position, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, auxDifference.normalized, dist, maskObstacle);

            //tra personaggio e il soldato ci sono ostacoli?
            if (hit.collider == null)
            {
                Debug.Log("preso");
            }

        }
    }
}
