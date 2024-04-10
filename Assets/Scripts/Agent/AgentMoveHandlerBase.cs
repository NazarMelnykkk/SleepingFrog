using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class AgentMoveHandlerBase : MonoBehaviour, ICommandHandler
{
    [Header("Components")]
    [SerializeField] private Agent _agent;
    [SerializeField] private AICommandHandler _enemyMoveHandler;
    [SerializeField] private AgentAnimationController _controller;

     private float _stoppingDistance = 0.2f;

    private StatsValue _moveSpeed;

    private Command _command;

    private void Start()
    {
        _moveSpeed = _agent.GetStatsValue(Statistic.MoveSpeed);
    }

    public void ProcessCommand(Command command)
    {
        _command = command;

        MoveToTarget();
    }

    void MoveToTarget()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _command.Points);

        if (distanceToPlayer <= _stoppingDistance)
        {
            _command.IsComplete = true;
            _controller.SetState(AnimationState.Idle);
            _enemyMoveHandler.AttackCommand();
            return;
        }
        else
        {
            _controller.SetState(AnimationState.Run);
        }

        float moveDirection = Mathf.Sign(_command.Points.x - transform.position.x);

        transform.Translate(Vector2.right * moveDirection * _moveSpeed.Float_value * Time.deltaTime);

    }
}
