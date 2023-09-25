using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int MaxHealth { get; }
    int CurrentHealth { get; }
    bool IsDead { get; }

    void TakeDamage(int damage);

    event Action DeathEvent;
    event Action DamageEvent;
}
