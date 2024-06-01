using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class Goal_Movement : Goal_Base
    {
        [SerializeField] int chasePriority = 30;
        DetectableTarget currentTarget;
        [SerializeField] int currentPriority;
        [SerializeField] bool currentblocking;
        [SerializeField] float distanceFromTarget;
        
        public Vector3 MoveTarget => currentTarget != null ? currentTarget.transform.position : transform.position;

        public override void OnTickGoal()
        {
            currentPriority = 0;
            currentblocking = blocking;

            if (enemyManager.currentTarget == null || currentblocking)
            {
                enemyManager.currentTarget = FindObjectOfType<PlayerStats>();
                return;
            }
            
            distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

            if (distanceFromTarget <= enemyManager.detectionRadius)
            {
                currentPriority = chasePriority;
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
                if (distanceFromTarget < enemyManager.detectionRadius)
                {
                    return true;
                }
            }
            return false;
        }
    }
}