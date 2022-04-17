using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private float _turboSpeed;
    private Rigidbody _rb;
    private Vector3 _movementVector;
    private Player _player;

    private void Awake()
    {
        _turboSpeed = speed * 2f;
        _rb = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameActive)
        {
            if (_player.team.user)
            {
                if (_player.user)
                {
                    if(Input.GetAxis("Turbo") == 0)
                        _rb.velocity = _movementVector * speed * Time.deltaTime;

                    if (Input.GetAxis("Turbo") > 0)
                        _rb.velocity = _movementVector * _turboSpeed * Time.deltaTime;
                    
                    if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                        transform.rotation = Quaternion.LookRotation(_movementVector);
                }
            }
        }
    }
}
