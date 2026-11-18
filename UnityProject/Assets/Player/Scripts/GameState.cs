using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int supplies = 0;

    public List<string> inventory = new List<string>();

    public void AddSupplies(int amount)
    {
        supplies += amount;
        Debug.Log("Supplies: " + supplies);
    }

    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
        Debug.Log("Collected: " + itemName);
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }
}
