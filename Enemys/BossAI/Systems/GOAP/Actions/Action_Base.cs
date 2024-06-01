using System.Collections;
using System.Collections.Generic;
using ST;
using UnityEngine;

public class Action_Base : MonoBehaviour
{
    protected EnemyManagerGoap enemyManager;
    protected EnemyAnimationManager enemyAnimationManager;

    protected Goal_Base LinkedGoal;

    void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManagerGoap>();
        enemyAnimationManager = FindObjectOfType<EnemyAnimationManager>();

    }

    public virtual List<System.Type> GetSupportedGoals()
    {
        return null;
    }

    public virtual float GetCost()
    {
        return 0f;
    }

    public virtual void OnActivated(Goal_Base _linkedGoal)
    {
        LinkedGoal = _linkedGoal;
    }

    public virtual void OnDeactivated()
    {
        LinkedGoal = null;
    }

    public virtual void OnTick()
    {

    }
}
