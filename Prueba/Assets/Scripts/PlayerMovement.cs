using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [Header("Referencias")]
    public Transform modelTransform;
    public Transform cameraPivot;

    [Header("Movimiento")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 10f;

    [Header("Salto")]
    public float jumpHeight = 2f;

    [Header("Gravedad")]
    public float gravity = -9.81f;

    private float verticalVelocity;

    private Vector2 moveInput;

    private bool isMoving;
    private bool isRunning;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        animator = modelTransform.GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("No se encontró Animator.");
        }
    }

    private void Update()
    {
        isMoving = moveInput.magnitude > 0.1f;

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 forward = cameraPivot.forward;
        Vector3 right = cameraPivot.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection =
            forward * moveInput.y +
            right * moveInput.x;

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(moveDirection);

            modelTransform.rotation =
                Quaternion.Slerp(
                    modelTransform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
        }

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity =
            moveDirection * currentSpeed;

        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        if (animator == null)
            return;

        animator.SetBool("isMoving", isMoving);

        animator.SetBool(
            "isRunning",
            isRunning && isMoving
        );
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            isRunning = true;

        if (context.canceled)
            isRunning = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            verticalVelocity =
                Mathf.Sqrt(
                    jumpHeight *
                    -2f *
                    gravity
                );

            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (animator != null)
            {
                animator.SetTrigger("Slide");
            }
        }
    }
}