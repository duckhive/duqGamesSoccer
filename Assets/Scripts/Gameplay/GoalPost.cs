using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        FeedbacksManager.Instance.hitPost.PlayFeedbacks();
    }
}
