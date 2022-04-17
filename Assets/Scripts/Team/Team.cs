using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TeamEnum
{
    Home,
    Away
}

public class Team : MonoBehaviour
{
    public List<Player> teamPlayers;
    public List<Player> currentPlayer;
    public ShotTarget shotTarget;
    public TeamEnum teamEnum;
    public int score;
    public bool user;

    
    private void Awake()
    {
        teamPlayers = GetComponentsInChildren<Player>().ToList();
        shotTarget = GetComponentInChildren<ShotTarget>();
    }
}
