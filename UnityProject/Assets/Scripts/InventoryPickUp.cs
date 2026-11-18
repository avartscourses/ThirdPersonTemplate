
using UnityEngine;

public class InventoryPickup : MonoBehaviour
{
    [SerializeField] private string itemName = "Compass";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameState playerState = other.GetComponent<GameState>();

            if (playerState != null)
            {
                playerState.AddItem(itemName);

                CollectionManager manager =
                    FindFirstObjectByType<CollectionManager>();

                if (manager != null)
                {
                    manager.ShowMessage(
                        itemName + " added to inventory!");
                }

                gameObject.SetActive(false);
            }
        }
    }
}
