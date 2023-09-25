using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBehavior : MonoBehaviour, IDamageable
{
    [SerializeField]
    [Min(1)]
    private int maxHealth = 10;

    public bool IsDead => CurrentHealth == 0;

    public int MaxHealth => maxHealth;

    public int CurrentHealth { get; private set; }

    public event Action DeathEvent;
    public event Action DamageEvent;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        DamageEvent?.Invoke();
        if (IsDead)
        {
            DeathEvent?.Invoke();
        }
    }
}
