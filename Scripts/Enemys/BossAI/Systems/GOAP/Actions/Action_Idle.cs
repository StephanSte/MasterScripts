using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Idle : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] {typeof(Goal_Idle)});

    private Goal_Idle _idleGoal;
    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override float GetCost()
    {
        return 0f;
    }
    
    public override void OnActivated(Goal_Base _linkedGoal)
    {
        base.OnActivated(_linkedGoal);

        // cache the chase goal
        _idleGoal = (Goal_Idle) LinkedGoal;

        enemyAnimationManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
        enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        _idleGoal = null;
    }
    
    public override void OnTick()
    {
        enemyAnimationManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
        enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
    }
}
