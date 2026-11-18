using UnityEngine;
using System.Collections;

public class DangerZone : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damagePerSecond = 10;

    [Header("Visual Feedback")]
    [SerializeField] private Renderer zoneRenderer;
    [SerializeField] private Color normalColor = new Color(1f, 0.5f, 0f);
    [SerializeField] private Color dangerColor = Color.red;

    private PlayerHealth playerHealth;
    private Coroutine damageCoroutine;
    private Material zoneMaterial;

    private void Start()
    {
        if (zoneRenderer != null)
        {
            zoneMaterial = zoneRenderer.material;
            zoneMaterial.color = normalColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health == null)
            return;

        playerHealth = health;

        if (zoneMaterial != null)
            zoneMaterial.color = dangerColor;

        damageCoroutine = StartCoroutine(ApplyDamage());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != playerHealth)
            return;

        StopDamage();
    }

    private IEnumerator ApplyDamage()
    {
        while (playerHealth != null)
        {
            playerHealth.TakeDamage(damagePerSecond);
            yield return new WaitForSeconds(1f);
        }
    }

    private void StopDamage()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }

        playerHealth = null;

        if (zoneMaterial != null)
            zoneMaterial.color = normalColor;
    }

    private void OnDisable()
    {
        StopDamage();
    }
}