using System.Collections;
using UnityEngine;

public class DamageWall : MonoBehaviour
{
    public float cooldown = 20f;
    public float damage = 1f;

    private bool canDamage = true;

    private Renderer[] renderers;
    private Collider[] colliders;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponents<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canDamage)
            return;

        SubmarineHealth health =
            collision.gameObject.GetComponent<SubmarineHealth>();

        if (health == null)
            return;

        health.TakeDamage(damage);

        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        canDamage = false;

        foreach (Renderer r in renderers)
            r.enabled = false;

        foreach (Collider c in colliders)
            c.enabled = false;

        yield return new WaitForSeconds(cooldown);

        foreach (Renderer r in renderers)
            r.enabled = true;

        foreach (Collider c in colliders)
            c.enabled = true;

        canDamage = true;
    }
}