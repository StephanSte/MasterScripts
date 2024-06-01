using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Attack : Goal_Base
{
    [SerializeField] int attackPriority = 50;
    DetectableTarget CurrentTarget;
    [SerializeField] int currentPriority;
    [SerializeField] bool currentblocking;
    [SerializeField] float distanceFromTarget;
    
    public Vector3 MoveTarget => CurrentTarget != null ? CurrentTarget.transform.position : transform.position;

    public override void OnTickGoal()
    {
        currentPriority = 0;
        currentblocking = blocking;
        if (enemyManager.canDoCombo)
        {
            currentPriority = attackPriority;
            return;
        }
        if (enemyManager.currentTarget == null || currentblocking || enemyManager.currentRecoveryTime > 0)
        {
            return;
        }
            
        distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        if (distanceFromTarget <= enemyManager.maximumAttackRadius)
        {
            currentPriority = attackPriority;
        }
    }

    public override void OnGoalDeactivated()
    {
        base.OnGoalDeactivated();
        
        CurrentTarget = null;
    }

    public override void OnGoalActivated(Action_Base _linkedAction)
    {
        base.OnGoalActivated(_linkedAction);
    }
    
    public override int CalculatePriority()
    {
        return currentPriority;
    }

    public override bool CanRun()
    {
        if (enemyManager.currentTarget && !blocking && !enemyManager.isInteracting && distanceFromTarget < enemyManager.maximumAttackRadius)
        {
            if (enemyManager.canDoCombo || enemyManager.currentRecoveryTime <= 0)
            {
                return true;
            }
        }
        return false;
    }
}
