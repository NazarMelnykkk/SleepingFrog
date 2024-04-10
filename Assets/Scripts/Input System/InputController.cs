using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController Instance;

    public static PlayerInputActions InputActions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InputActions = new PlayerInputActions();
        InputActions.Enable();
    }

    private void OnEnable()
    {
        InputActions.Character.AttackDirection.performed += OnLeftButtonPerformed;
    }

    public event Action<Vector2> OnAttackDirectionButtonPerformedEvent;
    private void OnLeftButtonPerformed(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        OnAttackDirectionButtonPerformedEvent?.Invoke(direction);
    }

}
