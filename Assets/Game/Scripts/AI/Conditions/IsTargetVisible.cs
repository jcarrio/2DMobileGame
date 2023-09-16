using BBUnity.Conditions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Condition("Game/Perception/IsTargetVisible")]
public class IsTargetVisible : GOCondition
{
    [InParam("Target")]
    private GameObject target;
    public override bool Check()
    {
        return Vector2.Distance(gameObject.transform.position, target.transform.position) < 3;
    }
}
