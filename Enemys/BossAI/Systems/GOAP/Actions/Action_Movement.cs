using System.Collections;
using System.Collections.Generic;
using ST;
using UnityEngine;

public class Action_Movement : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] {typeof(Goal_Movement)});

    Goal_Movement _movementGoal;
    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override float GetCost()
    {
        return 0;
    }

    public override void OnActivated(Goal_Base _linkedGoal)
    {
        base.OnActivated(_linkedGoal);

        // cache the chase goal
        _movementGoal = (Goal_Movement) LinkedGoal;

        MoveToTarget();
        //Audio audio = GetComponentInChildren<Audio>();
        //audio.playstarterClip(); // plays the first time the player engages the enemy
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();

        _movementGoal = null;
    }

    public override void OnTick()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget =
            Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        enemyManager.HandleRotateTowardsTarget();

        if (enemyManager.isPreformingAction)
        {
            if (enemyAnimationManager.anim.GetFloat("Horizontal") != 0 &&
                enemyAnimationManager.anim.GetFloat("Vertical") != 0)
            {
                enemyAnimationManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return;
            }
        }

        if (distanceFromTarget <= enemyManager.detectionRadius)
        {
            enemyAnimationManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
            enemyAnimationManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            return;
        }
        
        //for the first time
        enemyAnimationManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
        enemyAnimationManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
    }
}