using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Image healthBarFillImage;

    [SerializeField]
    private DamageableBehavior damageable;

    [SerializeField]
    [Min(0.1f)]
    private float speed = 2;

    private void LateUpdate()
    {
        float healthPercent = (float) damageable.CurrentHealth / damageable.MaxHealth;
        healthBarFillImage.fillAmount = Mathf.Lerp(healthBarFillImage.fillAmount, healthPercent, Time.deltaTime * speed);
    }
}
