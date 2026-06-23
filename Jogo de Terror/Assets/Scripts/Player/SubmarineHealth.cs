using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SubmarineHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    public Image healthFill;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthFill != null)
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
            0.25f
        );

        transform.DOShakePosition(
            0.2f,
            0.2f
        );

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Submarino destruído!");
    }
}