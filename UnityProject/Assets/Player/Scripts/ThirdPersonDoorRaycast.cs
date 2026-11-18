
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonDoorRaycast : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private float rayHeight = 1.5f;
    [SerializeField] private CollectionManager collectionManager;

    void Update()
    {
        if (Keyboard.current == null ||
            !Keyboard.current.eKey.wasPressedThisFrame)
            return;

        Vector3 origin =
            transform.position + Vector3.up * rayHeight;

        Ray ray = new Ray(origin, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit,
            interactionDistance, Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore))
        {
            Debug.Log("Ray hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Door"))
            {
                collectionManager.TryOpenDoor();
            }
        }
    }
}
