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

    [InParam("TargetMemoryDuration")]
    private float targetMemoryDuration;

    private float forgetTargetTime;

    public override bool Check()
    {
        bool isAvailable = IsAvailable();
        bool isAttacking = aiSense.IsTouched();

        if (isAttacking && isAvailable)
        {
            forgetTargetTime = Time.time + targetMemoryDuration;
            return true;
        }
        return Time.time < forgetTargetTime && isAvailable;
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
