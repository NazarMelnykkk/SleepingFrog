using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AICommandHandler : MonoBehaviour
{
    [SerializeField] private CommandHandler _commandHandler;
    [SerializeField] private AgentViewHandler _viewHandler;

    private Coroutine _attackCoroutite;
    private float _delay = 1.5f;

    private List<IDamageable> _targets = new();

    public void Init(Agent target)
    {    
        _targets.Add(target);

        if (target != null) 
        {          
            MoveToTarget(target.transform.position);
        }
    }

    private void MoveToTarget(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        _viewHandler.Flip(direction);
        MoveCommand(target);
    }

    private void MoveCommand(Vector2 direction)
    {
        _commandHandler.SetCommand(new Command(CommandType.Move, direction));
    }

    public void AttackCommand()
    {
        _attackCoroutite = StartCoroutine(AttackCoroutite());
    }

    private IEnumerator AttackCoroutite()
    {
        while (true)
        {
            _commandHandler.SetCommand(new Command(CommandType.Attack, _targets));

            yield return new WaitForSeconds(_delay);
        }

    }

    private void OnDisable()
    {
        if (_attackCoroutite != null)
        {
            StopCoroutine(_attackCoroutite);
        }
    }


}
