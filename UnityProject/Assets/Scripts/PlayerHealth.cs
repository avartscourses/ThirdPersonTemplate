using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private TMP_Text healthText;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || currentHealth <= 0)
            return;

        currentHealth = Mathf.Max(0, currentHealth - amount);

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth == 0)
        {
            Debug.Log("Player died!");
        }
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || currentHealth <= 0)
            return;

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        UpdateHealthUI();
    }
    private void UpdateHealthUI()
{
    if (healthText != null)
    {
        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }
}
}