using System.Collections;
using System.Collections.Generic;
using ST;
using UnityEngine;

public interface IGoal
{
    int CalculatePriority();
    bool CanRun();

    void OnTickGoal();
    void OnGoalActivated(Action_Base _linkedAction);
    void OnGoalDeactivated();
}

public class Goal_Base : MonoBehaviour, IGoal
{
    protected bool blocking = false;
    protected GOAPUI DebugUI;
    protected Action_Base LinkedAction;
    protected EnemyManagerGoap enemyManager;

    void Awake()
    {
    }

    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManagerGoap>();
        if (FindObjectOfType<GOAPUI>())
        {
            DebugUI = FindObjectOfType<GOAPUI>();
        }
        else
        {
            DebugUI = null;
        }
        
    }

    void Update()
    {
        OnTickGoal();

        if (DebugUI != null)
        {
            DebugUI.UpdateGoal(this, GetType().Name, LinkedAction ? "Running" : "Paused", CalculatePriority());
        }
    }

    public virtual int CalculatePriority()
    {
        return -1;
    }

    public virtual bool CanRun()
    {
        return false;
    }

    public virtual void OnTickGoal()
    {

    }

    public virtual void OnGoalActivated(Action_Base _linkedAction)
    {
        LinkedAction = _linkedAction;
    }

    public virtual void OnGoalDeactivated()
    {
        LinkedAction = null;
        blocking = false;
    }
}
