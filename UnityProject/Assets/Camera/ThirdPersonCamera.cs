using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Header("Camera")]
    [SerializeField] private float mouseSensitivity = 0.15f;
    [SerializeField] private float followSmoothness = 12f;

    [Header("Vertical Rotation")]
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;
    

    private float yaw;
    private float pitch;

    void Start()
    {
        if (target != null)
        {
            transform.position =
                target.position + Vector3.up * 1.5f;
        }

        yaw = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        if (target == null || Mouse.current == null)
            return;

        // 1. FOLLOW PLAYER
        Vector3 targetPosition =
            target.position + Vector3.up * 1.5f;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSmoothness * Time.deltaTime
        );

        // 2. MOUSE INPUT
        Vector2 mouseDelta =
            Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * mouseSensitivity;
        pitch -= mouseDelta.y * mouseSensitivity;

        pitch = Mathf.Clamp(
            pitch,
            minPitch,
            maxPitch
        );

        // 3. ROTATE CAMERA PIVOT
        transform.rotation =
            Quaternion.Euler(
                pitch,
                yaw,
                0f
            );
    }
}