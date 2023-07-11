using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.main.Enable();
    }

    public Vector2 getMovementInput()
    {
        return inputActions.main.Movement.ReadValue<Vector2>();
    }
}
