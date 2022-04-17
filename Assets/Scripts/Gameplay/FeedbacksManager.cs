using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class FeedbacksManager : MonoBehaviour
{
    public static FeedbacksManager Instance;

    public MMFeedbacks passBall;
    public MMFeedbacks gainPossession;
    public MMFeedbacks shootBall;
    public MMFeedbacks goalScored;
    public MMFeedbacks hitPost;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }
}
