using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject userIndicator;
    
    [HideInInspector] public Team team;
    [HideInInspector] public Vector3 startPosition;

    public Transform ballPosition;
    public bool user;
    public bool hasPossession;

    private Rigidbody _rb;
    
    private void Awake()
    {
        team = GetComponentInParent<Team>();
        startPosition = transform.position;

        _rb = GetComponent<Rigidbody>();
    }

    public void UserBrain()
    {
        user = true;
        team.currentPlayer.Insert(0, this);

        if(team.currentPlayer.Count > 2)
            team.currentPlayer.RemoveAt(2);
        
        userIndicator.SetActive(true);
    }

    public void AiBrain()
    {
        user = false;
        userIndicator.SetActive(false);
    }
    
    public void ResetPosition()
    {
        transform.position = startPosition;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
