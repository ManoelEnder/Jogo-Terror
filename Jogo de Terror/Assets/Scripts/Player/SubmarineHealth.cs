using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SubmarineHealth : MonoBehaviour
{
    [Header("Referências")]
    public SubmarineBreak submarineBreak;

    [Header("Vida")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    public Image healthFill;

    private bool dead;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthFill != null)
            healthFill.fillAmount = 1f;
    }

    public void TakeDamage(float damage)
    {
        if (dead)
            return;

        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthFill != null)
        {
            healthFill.DOFillAmount(
                currentHealth / maxHealth,
                0.3f
            );
        }

        Debug.Log("Vida: " + currentHealth);
       
        
        //chama a animacao de quebrar o submarino apos ficar com 0 de vida
        if (currentHealth <= 0)
        {
            dead = true;

            Debug.Log("Morreu");
            
            if (submarineBreak != null)
                submarineBreak.BreakSubmarine();
        }
    }
}