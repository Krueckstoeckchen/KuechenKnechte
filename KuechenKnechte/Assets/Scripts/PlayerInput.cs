using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputActions inputActions;
    public event Action OnInteract;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.main.Enable();
        inputActions.main.Interact.performed += OnInteractInput;

    }

    public Vector2 GetMovementInput()
    {
        return inputActions.main.Movement.ReadValue<Vector2>();
    }
    
    private void OnInteractInput(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }
}
