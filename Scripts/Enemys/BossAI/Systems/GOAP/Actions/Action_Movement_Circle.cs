using System.Collections;
using System.Collections.Generic;
using ST;
using UnityEngine;

public class Action_Movement_Circle : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] {typeof(Goal_Movement_Circle)});

    Goal_Movement_Circle _movementGoal;
    
    public float verticalMovementValue = 0;
    public float horizontalMovementValue = 0;

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
        horizontalMovementValue = Random.Range(-1, 1);
        // cache the chase goal
        _movementGoal = (Goal_Movement_Circle) LinkedGoal;

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
        enemyManager.HandleRotateTowardsTarget();
        verticalMovementValue = 0f;

        if (horizontalMovementValue <= 1 && horizontalMovementValue >= 0)
        {
            horizontalMovementValue = 0.5f;
        }
        else if (horizontalMovementValue >= -1 && horizontalMovementValue < 0)
        {
            horizontalMovementValue = -0.5f;
        }
        
        enemyAnimationManager.anim.SetFloat("Horizontal", horizontalMovementValue, 0.1f, Time.deltaTime);
        enemyAnimationManager.anim.SetFloat("Vertical", verticalMovementValue, 0.1f, Time.deltaTime);
    }
}