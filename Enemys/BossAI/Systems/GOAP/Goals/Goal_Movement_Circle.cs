using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class Goal_Movement_Circle : Goal_Base
    {
        [SerializeField] int circlePriority = 35;
        DetectableTarget currentTarget;
        [SerializeField] int currentPriority;
        [SerializeField] bool currentblocking;
        [SerializeField] float distanceFromTarget;

        public override void OnTickGoal()
        {
            currentPriority = 0;
            currentblocking = blocking;

            if (enemyManager.currentTarget == null || currentblocking)
            {
                return;
            }
            
            distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

            if (distanceFromTarget <= enemyManager.maximumAttackRadius)
            {
                currentPriority = circlePriority;
            }
        }

        public override void OnGoalDeactivated()
        {
            base.OnGoalDeactivated();

            currentTarget = null;
        }

        public override int CalculatePriority()
        {
            return currentPriority;
        }

        public override bool CanRun()
        {
            if (enemyManager.currentTarget && !blocking && !enemyManager.isInteracting)
            {
                if (distanceFromTarget < enemyManager.maximumAttackRadius)
                {
                    return true;
                }
            }
            return false;
        }
    }
}