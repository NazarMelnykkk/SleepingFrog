using System.Collections;
using UnityEngine;

public class AgentAttackHandler : MonoBehaviour, ICommandHandler
{
    [SerializeField] private Agent _agent;
    [SerializeField] private AgentAnimationController _agentAnimationController;

    private bool _isAttack = false;
    private float _attackCooldown = 0.3f;

    private Command _command;

    private void Awake()
    {
        if (_agent == null)
        {
            _agent = GetComponent<Agent>();
        }

        if (_agentAnimationController == null)
        {
            _agentAnimationController = GetComponent<AgentAnimationController>();
        }
    }

    public void ProcessCommand(Command command)
    {
        _command = command;

        if (_isAttack == true)
        {
            return;
        }

        _isAttack = true;
        _agentAnimationController.SetState(AnimationState.Attack);
    }

    public void DealDamage()
    {
        foreach (var target in _command.Targets)
        {
            if (target != null)
            {
                int damage = _agent.GetDamage();
                target.TakeDamage(damage);
            }
        }
    }

    public void EndOfAttack()
    {
        _command.IsComplete = true;
        _agentAnimationController.SetState(AnimationState.Idle);
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _isAttack = false;
        
    }
   
}
