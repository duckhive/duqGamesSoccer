using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Team _team;

    private void Awake()
    {
        _team = GetComponentInParent<Team>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>() != null)
        {
            _team.score++;
            StartCoroutine(GameManager.Instance.GoalScored());
            FeedbacksManager.Instance.goalScored.PlayFeedbacks();
        }
    }
}
