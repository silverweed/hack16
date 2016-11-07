using UnityEngine;
using System.Collections;

public class Npc : MonoBehaviour
{
    public Vector2 direction;
    protected bool canSeePlayer;
    public virtual bool CanSeePlayer
    {
        get { return canSeePlayer; }
        set
        {
            canSeePlayer = value;
        }
    }
}
