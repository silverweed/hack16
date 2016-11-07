using UnityEngine;
using System.Collections;

public class Npc : MonoBehaviour
{
    public Vector2 direction;
    private bool canSeePlayer;
    public bool CanSeePlayer
    {
        get { return canSeePlayer; }
        set
        {
            canSeePlayer = value;
        }
    }
}
