using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputActions inputActions;
    public event Action OnInteract;
    public event Action OnInteractAlternate;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.main.Enable();
        inputActions.main.Interact.performed += OnInteractInput;
        inputActions.main.InteractAlternate.performed += OnInteractAlternateInput;

    }

    public Vector2 GetMovementInput()
    {
        return inputActions.main.Movement.ReadValue<Vector2>();
    }
    
    private void OnInteractInput(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

    private void OnInteractAlternateInput(InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke();
    }
}
