using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{

    [SerializeField] private CommandHandler _commandHandler;
    [SerializeField] private AgentAttackObserver _agentAttackObserver;

    [SerializeField] private AgentViewHandler _viewHandler;


    private void OnEnable()
    {
        InputController.Instance.OnAttackDirectionButtonPerformedEvent += GetButton;
    }

    private void OnDisable()
    {
        InputController.Instance.OnAttackDirectionButtonPerformedEvent -= GetButton;
    }

    private void GetButton(Vector2 direction)
    {
        _viewHandler.Flip(direction);
        AttackCommand(direction);
    }

    private void AttackCommand(Vector2 direction)
    {
        _commandHandler.SetCommand(new Command(CommandType.Attack, _agentAttackObserver.CheckTargets(direction)));
    }
}
