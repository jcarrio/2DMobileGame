using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IDamageable))]
[RequireComponent(typeof(EnemyAIController))]

public class EnemyAnimationController : CharacterAnimationController
{
    private IDamageable damageable;
    EnemyAIController aiController;

    protected override void Awake()
    {
        base.Awake();
        aiController = GetComponent<EnemyAIController>();

        damageable = GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.DeathEvent += OnDeath;
        }
    }

    protected override void Update()
    {
        base.Update();
        animator.SetBool(EnemyAnimationKeys.IsAttacking, aiController.IsAttacking);
        animator.SetBool(EnemyAnimationKeys.IsChasing, aiController.IsChasing && !aiController.IsAttacking);
    }

    private void OnDeath()
    {
        animator.SetTrigger(EnemyAnimationKeys.TriggerDead);
    }

}
