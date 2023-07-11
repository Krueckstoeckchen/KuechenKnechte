using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    new private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 movementInput = new Vector2(0f, 0f);
        if (Input.GetKey(KeyCode.W))
        {
            movementInput.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementInput.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementInput.x = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementInput.x = -1;
        }
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
