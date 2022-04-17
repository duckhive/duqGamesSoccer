using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball Instance;
    
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Vector3 startPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    private void Update()
    {
        OutOfBounds();
    }

    public void ResetBall()
    {
        transform.position = startPosition;
        transform.SetParent(null);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void OutOfBounds()
    {
        if (transform.position.y < 0)
            GameManager.Instance.ResetBallAndPlayers();
    }
}
