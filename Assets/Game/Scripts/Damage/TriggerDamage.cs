using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField]
    [Min(0)]
    private int damage = 1;

    [SerializeField]
    private float damageInterval = 1.0f;

    private bool stopDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            stopDamage = false;
            //collision.GetComponent<PlayerController>().StartCoroutine
            StartCoroutine(takeDamage(damageable));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            stopDamage = true;
        }
    }

    IEnumerator takeDamage(IDamageable damageable)
    {
        while (!damageable.IsDead && !stopDamage)
        {
            damageable.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
