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
        Vector2 movementInput = playerInput.GetMovementInput();
        movementInput *= speed;
        Vector3 movement = new Vector3(movementInput.x, rigidbody.velocity.y, movementInput.y);
        rigidbody.velocity = movement;
        if (movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(movementInput.x, 0, movementInput.y), new Vector3(0f,1f,0f));
            rigidbody.rotation = targetRotation;
        }
    }
}
