using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    new private Rigidbody rigidbody;

    private PlayerInput playerInput;

    
    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        playerInput = this.gameObject.GetComponent<PlayerInput>();
    }

    void Update()
    {
        Vector2 movementInput = playerInput.getMovementInput();
        
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        movement = movement.normalized;
        rigidbody.velocity = movement * speed;
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rigidbody.rotation = targetRotation;
        }
    }
}
