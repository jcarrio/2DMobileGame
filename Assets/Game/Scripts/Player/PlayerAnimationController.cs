using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IDamageable))]
[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : CharacterAnimationController
{
    private IDamageable damageable;
    private PlayerController player;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerController>();

        damageable = GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.DeathEvent += OnDeath;
        }
    }

    private void OnDestroy()
    {
        if (damageable != null)
        {
            damageable.DeathEvent -= OnDeath;
        }
    }

    protected override void Update()
    {
        base.Update();
        animator.SetBool(CharacterMovementAnimationKeys.IsCrouching, characterMovement.IsCrouching);
        animator.SetFloat(CharacterMovementAnimationKeys.VerticalSpeed, characterMovement.CurrentVelocity.y / characterMovement.JumpSpeed);
        animator.SetBool(CharacterMovementAnimationKeys.IsGrounded, characterMovement.IsGrounded);
        animator.SetBool(CharacterMovementAnimationKeys.IsAttacking, player.Weapon.IsAttacking);
    }

    private void OnDeath()
    {
        animator.SetTrigger(CharacterMovementAnimationKeys.TriggerDead);
    }
}
