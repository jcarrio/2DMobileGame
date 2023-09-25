using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;

[Action("Game/AttackTarget")]
public class AttackTarget : BasePrimitiveAction
{
    [InParam("Target")]
    private GameObject target;

    [InParam("AIController")]
    private EnemyAIController aiController;

    public override void OnStart()
    {
        base.OnStart();
        aiController.IsAttacking = true;
        //        charMovement.MaxGroundSpeed = attackSpeed;
    }

    public override void OnAbort()
    {
        base.OnAbort();
        aiController.IsAttacking = false;
    }

    public override TaskStatus OnUpdate()
    {
//        Debug.Log(aiController.IsAttacking);
        if (target == null)
        {
            return TaskStatus.ABORTED;
        }

        Vector2 toTarget = target.transform.position - aiController.transform.position;
        aiController.MovementInput = new Vector2(Mathf.Sign(toTarget.x), 0);
        return TaskStatus.RUNNING;
    }
}
