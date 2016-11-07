using UnityEngine;
using System.Collections;

public class Cameraman : Npc
{
    public MovingAxis movingAxis;
    public Transform minLimit;
    public Transform maxLimit;
    public float startFollowDistance = 1;
    public float followSpeed = 1;
    public Direction lookDirection;

    public enum MovingAxis { Horizontal, Vertical }

    private MovePlayer player;
    private enum State { Stopped, Follow }
    private State state;

    private void Start()
    {
        player = FindObjectOfType<MovePlayer>();
        state = State.Stopped;
        direction = dictonaryVector[lookDirection];
    }

    private void Update()
    {
        if (state == State.Stopped &&
            Vector2.Distance(player.transform.position, transform.position) < startFollowDistance)
        {
            state = State.Follow;
            Debug.Log("FOLLOW");
        }
        else if (state == State.Follow &&
            Vector2.Distance(player.transform.position, transform.position) > startFollowDistance)
        {
            state = State.Stopped;
            Debug.Log("STOPPED");
        }

        if (state == State.Follow)
        {
            float move = followSpeed * Time.deltaTime;
            if (movingAxis == MovingAxis.Horizontal)
            {
                float dir = Mathf.Sign(player.transform.position.x - transform.position.x);
                transform.Translate(Vector2.right * dir * move);
                if (transform.position.x > maxLimit.position.x)
                    transform.position = new Vector3(maxLimit.position.x, transform.position.y, transform.position.z);
                else if (transform.position.x < minLimit.position.x)
                    transform.position = new Vector3(minLimit.position.x, transform.position.y, transform.position.z);
            }
            else if (movingAxis == MovingAxis.Vertical)
            {
                float dir = Mathf.Sign(player.transform.position.y - transform.position.y);
                transform.Translate(Vector2.up * dir * move);
                if (transform.position.y > maxLimit.position.y)
                    transform.position = new Vector3(transform.position.x, maxLimit.position.y, transform.position.z);
                else if (transform.position.y < minLimit.position.y)
                    transform.position = new Vector3(transform.position.x, minLimit.position.y, transform.position.z);
            }
        }
    }
}
