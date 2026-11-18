using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform cameraTransform;

    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float fallLimit = -5f;

    private CharacterController controller;
    private Animator animator;

    private float verticalVelocity;

    private static readonly int SpeedHash =
        Animator.StringToHash("Speed");

    private static readonly int JumpHash =
        Animator.StringToHash("Jump");

    private static readonly int GroundedHash =
        Animator.StringToHash("Grounded");

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError(
                "Δεν βρέθηκε Animator σε child του Player."
            );
        }
    }

    void Update()
    {
        // Respawn if the player falls out of the level
        if (transform.position.y < fallLimit)
        {
            controller.enabled = false;

            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;

            verticalVelocity = 0f;

                controller.enabled = true;

            return;
        }
        // -------------------------
        // 1. INPUT
        // -------------------------

        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
                input.y += 1f;

            if (Keyboard.current.sKey.isPressed)
                input.y -= 1f;

            if (Keyboard.current.dKey.isPressed)
                input.x += 1f;

            if (Keyboard.current.aKey.isPressed)
                input.x -= 1f;
        }

        // -------------------------
        // 2. DIRECTION
        // -------------------------

        Vector3 cameraForward = cameraTransform.forward;
Vector3 cameraRight = cameraTransform.right;

cameraForward.y = 0f;
cameraRight.y = 0f;

cameraForward.Normalize();
cameraRight.Normalize();

Vector3 direction =
    cameraForward * input.y +
    cameraRight * input.x;

        if (direction.sqrMagnitude > 1f)
        {
            direction.Normalize();
        }

        bool isMoving =
            direction.sqrMagnitude > 0.01f;

        // -------------------------
        // 3. WALK / RUN
        // -------------------------

        bool isRunning =
            isMoving &&
            Keyboard.current != null &&
            Keyboard.current.leftShiftKey.isPressed;

        float currentMoveSpeed =
            isRunning ? runSpeed : walkSpeed;

        // -------------------------
        // 4. ROTATION
        // -------------------------

        if (isMoving)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
        }

        // -------------------------
        // 5. GROUND CHECK
        // -------------------------

        bool isGrounded = controller.isGrounded;

        if (animator != null)
        {
            animator.SetBool(
                GroundedHash,
                isGrounded
            );
        }

        if (isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        // -------------------------
        // 6. JUMP
        // -------------------------

        if (
            isGrounded &&
            Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame
        )
        {
            verticalVelocity =
                Mathf.Sqrt(
                    jumpHeight * -2f * gravity
                );

            if (animator != null)
            {
                animator.SetTrigger(JumpHash);
            }
        }

        // -------------------------
        // 7. GRAVITY
        // -------------------------

        verticalVelocity +=
            gravity * Time.deltaTime;

        // -------------------------
        // 8. MOVEMENT
        // -------------------------

        Vector3 velocity =
            direction * currentMoveSpeed;

        velocity.y = verticalVelocity;

        controller.Move(
            velocity * Time.deltaTime
        );

        // -------------------------
        // 9. LOCOMOTION ANIMATION
        // -------------------------

        float targetAnimationSpeed = 0f;

        if (isMoving)
        {
            targetAnimationSpeed =
                isRunning ? 1f : 0.5f;
        }

        if (animator != null)
        {
            animator.SetFloat(
                SpeedHash,
                targetAnimationSpeed,
                0.15f,
                Time.deltaTime
            );
        }
    }
}