using System.Linq;
using UnityEngine;

public class PlayerSwitching : MonoBehaviour
{
    [HideInInspector] public Team team;

    private void Awake()
    {
        team = GetComponent<Team>();
    }

    private void Update()
    {
        if (GameManager.Instance.gameActive)
        {
            if (team.user)
            {
                if (!team.currentPlayer[0].hasPossession)
                {
                    if (Input.GetButtonUp("Pass"))
                    {
                        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                            SwitchToPlayerClosestToBall();

                        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                            SwitchToPlayerInDirection();
                    }
                }
            }
        }
    }

    private Vector3 InputDirection()
    {
        var xInput = Input.GetAxis("Horizontal");
        var zInput = Input.GetAxis("Vertical");
        var forward = Camera.main.transform.forward;
        forward.y = 0;
        var right = Camera.main.transform.right;
        right.y = 0;

        return (right * xInput + forward * zInput).normalized;
    }

    private Vector3 DirectionTo(Player to, Player from)
    {
        return Vector3.Normalize(to.transform.position - from.transform.position);
    }
    
    public void SelectPlayerOnStart()
    {
        var smallestDistance = team.teamPlayers
            .OrderBy(t => Vector3.Distance(Ball.Instance.transform.position, t.transform.position)).FirstOrDefault();

        if (smallestDistance != null)
            smallestDistance.UserBrain();
    }
    
    private void SwitchToPlayerClosestToBall()
    {
        var smallestDistance = team.teamPlayers
            .OrderBy(t => Vector3.Distance(Ball.Instance.transform.position, t.transform.position)).FirstOrDefault();

        if (smallestDistance != null && smallestDistance != team.currentPlayer[0])
        {
            smallestDistance.UserBrain();
            team.currentPlayer[1].AiBrain();
        }
    }

    private void SwitchToPlayerInDirection()
    {
        var smallestAngle = team.teamPlayers
            .Where(t => t != team.currentPlayer[0])
            .OrderBy(t => Vector3.Angle(InputDirection(), DirectionTo(t, team.currentPlayer[0])))
            .FirstOrDefault();

        if (smallestAngle != null)
        {
            smallestAngle.UserBrain();
            team.currentPlayer[1].AiBrain();
        }
    }
}