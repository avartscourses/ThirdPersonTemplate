using UnityEngine;

public class CollectibleInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectionManager manager =
                FindFirstObjectByType<CollectionManager>();

            if (manager != null)
            {
                manager.AddCollectible();
                gameObject.SetActive(false);
            }
        }
    }
}