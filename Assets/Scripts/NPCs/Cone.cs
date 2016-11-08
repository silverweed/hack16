﻿using UnityEngine;
using System.Collections;
using System;

public class Cone : MonoBehaviour
{
    public float angleCone;
    public float distanceCone;
    public float damagePerSecond = 20f;

    private SpriteRenderer coneSprite;
    private LayerMask maskObstacle;
    private Npc owner;

    // Use this for initialization
    void Awake ()
    {
        owner = GetComponent<Npc>();
    }


    void Start()
    {
	maskObstacle = LayerMask.NameToLayer("Obstacle");
	coneSprite = transform.FindChild("Cone").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
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
                PlayerSeen();
            }

            owner.CanSeePlayer = !Convert.ToBoolean(hit.collider);
        }

	AdjustDirection();
    }

    void PlayerSeen()
    {
	    UIManager.Instance.Damage.Active = true;
	    UIManager.Instance.Stressbar.Damage(Time.deltaTime * damagePerSecond);
    }

    void AdjustDirection() {
	if (owner.direction == Vector2.up)
		coneSprite.transform.eulerAngles = new Vector3(0, 0, -90);
	else if (owner.direction == Vector2.down)
		coneSprite.transform.eulerAngles = new Vector3(0, 0, 90);
	else if (owner.direction == Vector2.left)
		coneSprite.transform.eulerAngles = new Vector3(0, 0, 0);
	else if (owner.direction == Vector2.right)
		coneSprite.transform.eulerAngles = new Vector3(0, 0, 180);
    }
}
