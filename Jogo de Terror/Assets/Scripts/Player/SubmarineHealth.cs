using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SubmarineHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    public Image healthFill;

    private void Start()
    {
        currentHealth = maxHealth;

        healthFill.fillAmount = 1f;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(
            currentHealth,
            0,
            maxHealth
        );

        healthFill.DOFillAmount(
            currentHealth / maxHealth,
            0.3f
        );

        if (currentHealth <= 0)
        {
            Debug.Log("Morreu");
        }
    }
}