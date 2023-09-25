using BBUnity.Conditions;
using Pada1.BBCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Condition("Game/Perception/IsTargetTouched")]
public class IsTargetTouched : GOCondition
{
    [InParam("Target")]
    private GameObject target;

    [InParam("PlayerTouched")]
    private AISense aiSense;

    public override bool Check()
    {
        bool isAvailable = IsAvailable();
        bool isAttacking = aiSense.IsTouched();
        return isAttacking && isAvailable;
    }

    private bool IsAvailable()
    {
        if (target == null)
        {
            return false;
        }
        //TODO: Não chamar getcomponent no update
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            return !damageable.IsDead;
        }
        return true;
    }
}
