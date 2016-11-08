using UnityEngine;
using System.Collections.Generic;

//Classe che muove il player

public class MovePlayer : MonoBehaviour
{
    public static Transform player;

    [Range(0, 10f)]
    public float speed;

    [Range(1, 5f)]
    public int countFramePastFromDirection;

    static public Vector2 directionPlayer;
    private Rigidbody2D rigidibodyPlayer;
    private Vector2 vectorInput;
    private List<Vector2> directions;

    private Animator animator;

    private void Awake()
    {
        player = this.transform;
        rigidibodyPlayer = GetComponent<Rigidbody2D>();
        directions = new List<Vector2>();
        directionPlayer = new Vector2(Mathf.Clamp(directionPlayer.x, -1, 1), Mathf.Clamp(directionPlayer.y, -1, 1));

        for (int i = 0; i < countFramePastFromDirection; i++)
        {
            directions.Add(vectorInput.normalized);
        }

        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        vectorInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (vectorInput != Vector2.zero)
        {
            animator.SetBool("New Bool", true); // is moving (in Unity 4.4.2 non riesco a rinominare i parametri -_-")
            vectorInput *= (speed * Time.fixedDeltaTime);
            rigidibodyPlayer.MovePosition(rigidibodyPlayer.position + vectorInput);
            AdjustList();

            //prende la direzione di 3 frame fa (questo per evitare problemi di imprecisione con il device)
            directionPlayer = directions[0];
            SetAnimatorDirection();

	    //GetComponent<SpriteRenderer>().flipX = vectorInput.x < 0; <-- non serve più: gestito nell'animazione
        }
        else
            animator.SetBool("New Bool", false);

        //DEBUG
        Debug.DrawLine(transform.position, transform.position + (Vector3)directionPlayer, Color.black);
    }

    private void AdjustList()
    {
        directions.RemoveAt(0);
        directions.Add(vectorInput.normalized);
    }

    private void SetAnimatorDirection()
    {
        float threshold = 0.1f;
        if (directionPlayer.x > threshold && Mathf.Abs(directionPlayer.y) < threshold)
            animator.SetInteger("New Int", 0); // right
        else if (Mathf.Abs(directionPlayer.x) < threshold && directionPlayer.y > threshold)
            animator.SetInteger("New Int", 1); // up
        else if (directionPlayer.x < -threshold && Mathf.Abs(directionPlayer.y) < threshold)
            animator.SetInteger("New Int", 2); // left
        else if (Mathf.Abs(directionPlayer.x) < threshold && directionPlayer.y < -threshold)
            animator.SetInteger("New Int", 3); // down
    }
}
