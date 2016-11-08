using UnityEngine;
using System.Collections;
using System;

public class Cone : MonoBehaviour
{
    public GameObject bubble;
    public float angleCone;
    public float distanceCone;
    public float damagePerSecond = 20f;

    private bool bubbleBool;
    private GameObject actualBubble;
    private Transform bubblePosition;
    private SpriteRenderer coneSprite;
    private LayerMask maskObstacle;
    private Npc owner;

    // Use this for initialization
    void Awake ()
    {
        bubbleBool = false;
        owner = GetComponent<Npc>();
        maskObstacle = LayerMask.NameToLayer("Obstacle");
        coneSprite = transform.FindChild("Cone").GetComponent<SpriteRenderer>();
        coneSprite.transform.localScale = new Vector3(coneSprite.transform.localScale.x * distanceCone, coneSprite.transform.localScale.y * distanceCone, 0);
        bubblePosition = transform.parent.FindChild("Bubble position");
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 auxDifference = MovePlayer.player.position - transform.position;
        Vector2 directionEnemy = GetComponent<Npc>().direction;

        //Il personaggio è nel cono visuale?
        owner.CanSeePlayer = false;
        if (Vector3.Angle(directionEnemy, auxDifference) < angleCone / 2 && auxDifference.magnitude < distanceCone)
        {
            float dist = Vector2.Distance(MovePlayer.player.position, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, auxDifference.normalized, dist, maskObstacle);

            //tra personaggio e il soldato ci sono ostacoli?
            if (hit.collider == null)
            {
                if (!bubbleBool)
                {
                    actualBubble = (GameObject)GameObject.Instantiate(bubble, bubblePosition.position, Quaternion.identity, transform.parent);
                    Invoke("DestroyBubble", 1.0f);
                    bubbleBool = true;
                }
                PlayerSeen();
            }

            owner.CanSeePlayer = !Convert.ToBoolean(hit.collider);
        }

	    AdjustDirection();
    }

    void DestroyBubble()
    {
        GameObject.Destroy(actualBubble);
        bubbleBool = false;
    }

    void PlayerSeen()
    {
	    UIManager.Instance.Damage.Active = true;
	    UIManager.Instance.Stressbar.Damage(Time.deltaTime * damagePerSecond);
    }

    void AdjustDirection()
    {
        var angle = Mathf.Atan2(owner.direction.y, owner.direction.x) * Mathf.Rad2Deg;
        coneSprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
