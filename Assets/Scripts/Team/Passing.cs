using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Passing : MonoBehaviour
{
    [SerializeField] private float passForce;
    [SerializeField] private float passAccel;
    
    private Team _team;
    private bool _passBegun;
    private float _resetPassForce;
    private float _passTimer;

    private void Awake()
    {
        _team = GetComponent<Team>();
        _resetPassForce = passForce;
    }

    private void Update()
    {
        PassHandler();
    }

    private void PassHandler()
    {
        if (GameManager.Instance.gameActive)
        {
            if (_team.user && _team.currentPlayer[0].hasPossession)
            {
                if (Input.GetButtonDown("Pass"))
                    _passBegun = true;

                if (Input.GetButton("Pass"))
                {
                    passForce += passAccel * Time.deltaTime;
                    _passTimer += Time.deltaTime;
                }
                
                if (_passBegun)
                {
                    if (Input.GetButtonUp("Pass") || _passTimer > 1)
                    {
                        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                            PassToClosestPlayer();

                        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                            PassToPlayerInDirection();
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
    
    private void PassToClosestPlayer()
    {
        var smallestDistance = _team.teamPlayers
            .Where(t => t != _team.currentPlayer[0])
            .OrderBy(t => Vector3.Distance(_team.currentPlayer[0].transform.position, t.transform.position)).FirstOrDefault();

        var direction = (smallestDistance.transform.position - Ball.Instance.transform.position).normalized;
        
        PassBall(direction);
    }

    private void PassToPlayerInDirection()
    {
        var smallestAngle = _team.teamPlayers
            .Where(t => t != _team.currentPlayer[0])
            .OrderBy(t => Vector3.Angle(InputDirection(), DirectionTo(t, _team.currentPlayer[0])))
            .FirstOrDefault();

        var direction = (smallestAngle.transform.position - Ball.Instance.transform.position).normalized;
        
        PassBall(direction);
    }
    
    private IEnumerator ChangeLayerToIgnoreCollisions(Player player)
    {
        player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        
        yield return new WaitForSeconds(0.2f);
        
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void PassBall(Vector3 direction)
    {
        Ball.Instance.rb.constraints = RigidbodyConstraints.None;
        Ball.Instance.transform.SetParent(null);
        StartCoroutine(ChangeLayerToIgnoreCollisions(_team.currentPlayer[0]));
        Ball.Instance.rb.AddForce(direction * passForce, ForceMode.Impulse);
        FeedbacksManager.Instance.passBall.PlayFeedbacks();
        _team.currentPlayer[0].hasPossession = false;
        _passBegun = false;
        passForce = _resetPassForce;
        _passTimer = 0;
    }
}