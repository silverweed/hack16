using UnityEngine;
using System;
using System.Collections.Generic;

public class MainCamera : MonoBehaviour
{
    [Range(0, 10)]
    public float distance;

    [Range(0, 1)]
    public float smooth;

    private Transform transformPlayer;
    private Rigidbody2D rigidbodyCamera;

    private Vector3 auxPositionCamera;
    private Vector2 auxVector;


    // Use this for initialization
    private void Awake()
    {
        rigidbodyCamera = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void FixedUpdate ()
    {
        if (transformPlayer != null) //DEBUG
        {
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        auxPositionCamera = PositionLocking();

        auxPositionCamera += TargetFocus();
        auxPositionCamera = LerpSmoothing();

        rigidbodyCamera.MovePosition(auxPositionCamera);
    }

    private Vector3 PositionLocking()
    {
        return transformPlayer.position;
    }

    private Vector3 TargetFocus()
    {
        auxVector = MovePlayer.directionPlayer;
        auxVector = new Vector2(auxVector.x * Camera.main.aspect, auxVector.y);
        return distance * auxVector; 
    }

    private Vector3 LerpSmoothing()
    {
        return Vector3.Lerp(rigidbodyCamera.position, auxPositionCamera, smooth);
    }
}
