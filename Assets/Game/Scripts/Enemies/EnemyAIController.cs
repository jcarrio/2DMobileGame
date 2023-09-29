using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;
using System.Runtime.Remoting.Messaging;
using BBUnity.Actions;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(TriggerDamage))]
[RequireComponent(typeof(IDamageable))]
public class EnemyAIController : MonoBehaviour
{
    CharacterMovement2D enemyMovement;
    CharacterFacing2D enemyFacing;
    TriggerDamage damager;
    IDamageable damageable;

    private Vector2 movementInput;
    private bool isChasing;
    private bool isAttacking;

    public bool IsChasing
    {
        get => isChasing;
        set => isChasing = value;
    }

    public bool IsAttacking
    {
        get => isAttacking;
        set => isAttacking = value;
    }

    public Vector2 MovementInput
    {
        get { return movementInput; }
        set { movementInput = new Vector2(Mathf.Clamp(value.x, -1, 1), Mathf.Clamp(value.y, -1, 1)); }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<CharacterMovement2D>();
        enemyFacing = GetComponent<CharacterFacing2D>();
        damager = GetComponent<TriggerDamage>();
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

    // Update is called once per frame
    void Update()
    {
        enemyMovement.ProcessMovementInput(movementInput);
        enemyFacing.UpdateFacing(movementInput, null);
    }

    private void OnDeath()
    {
        enabled = false;
        enemyMovement.StopImmediately();
        damager.gameObject.SetActive(false);
        Destroy(gameObject, 0.7f);
    }
}
