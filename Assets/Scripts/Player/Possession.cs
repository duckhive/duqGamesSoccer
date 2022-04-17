using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    private Team _team;
    private Player _player;

    private void Awake()
    {
        _team = GetComponentInParent<Team>();
        _player = GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<Ball>() && GameManager.Instance.gameActive)
        {
            GainPossession();
            FeedbacksManager.Instance.gainPossession.PlayFeedbacks();
        }
    }

    private void GainPossession()
    {
        Ball.Instance.transform.position = _player.ballPosition.position;
        Ball.Instance.transform.SetParent(_player.ballPosition);
        Ball.Instance.rb.constraints = RigidbodyConstraints.FreezeAll;
        _player.hasPossession = true;

        if(_player.team.user)
            SwapBrains();
    }

    private void SwapBrains()
    {
        if (_team.currentPlayer[0] != _player)
        {
            _player.UserBrain();
            _team.currentPlayer[1].AiBrain();
        }
    }
}
