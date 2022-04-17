using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float shotForce;

    private Player _player;
    private float _resetShotForce;
    private float _shotTimer;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _resetShotForce = shotForce;
    }

    private void Update()
    {
        if (GameManager.Instance.gameActive)
        {
            if (_player.hasPossession && _player.user && _player.team.user && Input.GetButton("Shoot"))
            {
                AimTarget();
                _shotTimer += Time.deltaTime;
                shotForce += Time.deltaTime * 10;
            }

            if (_player.hasPossession && _player.user && _player.team.user &&
                (Input.GetButtonUp("Shoot") || _shotTimer > 0.75f))
            {
                ShootBall();
                FeedbacksManager.Instance.shootBall.PlayFeedbacks();
                shotForce = _resetShotForce;
                _shotTimer = 0;
                _player.team.shotTarget.ResetPosition();
            }
        }
    }

    private void ShootBall()
    {
        var direction = (_player.team.shotTarget.transform.position - Ball.Instance.transform.position).normalized;
        
        Ball.Instance.rb.constraints = RigidbodyConstraints.None;
        Ball.Instance.transform.SetParent(null);
        Ball.Instance.rb.AddForce(direction * shotForce, ForceMode.Impulse);
        _player.hasPossession = false;
    }

    private void AimTarget()
    {
        var shotTarget = _player.team.shotTarget;
        
        shotTarget.transform.Translate(Vector3.up * Time.deltaTime * 20f);
        
        var xInput = Input.GetAxis("Horizontal");
        
        if(xInput != 0)
            shotTarget.transform.Translate(shotTarget.transform.right * 50 * xInput * Time.deltaTime);
    }
}
