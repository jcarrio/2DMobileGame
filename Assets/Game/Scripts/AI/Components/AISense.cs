using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISense : MonoBehaviour
{
    bool playerTouched = false;

    public bool IsTouched()
    {
        return playerTouched;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            playerTouched = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            playerTouched = false;
        }
    }
}
