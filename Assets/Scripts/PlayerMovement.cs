using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // variables
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;

    private Vector3 forwardDirection, rightDirection;
    private InputAction moveAction;

    void Awake()
    {
        moveAction = new InputAction("Move", InputActionType.Value);

        // WASD
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        // Flechas
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");

        // Gamepad
        moveAction.AddBinding("<Gamepad>/leftStick");
    }

    void OnEnable()  => moveAction.Enable();
    void OnDisable() => moveAction.Disable();

    void Start()
    {
        forwardDirection = Camera.main.transform.forward;
        forwardDirection.y = 0;
        forwardDirection = forwardDirection.normalized;

        rightDirection = Camera.main.transform.right;
        rightDirection.y = 0;
        rightDirection = rightDirection.normalized;
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        float horizontalInput = input.x;
        float verticalInput = input.y;

        Vector3 direction = forwardDirection * verticalInput + rightDirection * horizontalInput;

        if (direction.magnitude > 0.1f)
        {
            // Move the player
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Rotate the player to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }
}