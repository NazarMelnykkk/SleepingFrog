using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{

    [SerializeField] private AttackHandler _attackHandler;
    [SerializeField] private CharacterViewHandler _characterViewHandler;

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

        _characterViewHandler.Flip(direction);
    }
}
