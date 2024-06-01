using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Charge_Attack : Goal_Base
{
    [SerializeField] int ChargePriority = 30;
    [SerializeField] float MinAwarenessToChase = 1f;
    [SerializeField] float AwarenessToStopChase = 1f;
    [SerializeField] float PriorityBuildRate = 1.5f;
    [SerializeField] float PriorityDecayRate = 0.1f;
    [SerializeField] private float stoppingDistance = 6f;
    [SerializeField] private float distanceBetween = 0;
    [SerializeField] private float CurrentPriority = 0f;
    DetectableTarget CurrentTarget;

    public override void OnTickGoal()
    {/*
        // no targets
        if (Sensors.ActiveTargets == null || Sensors.ActiveTargets.Count == 0)
            return;
        
        if (CurrentTarget != null)
        {
            // check if the current is still sensed
            foreach (var candidate in Sensors.ActiveTargets.Values)
            {
                if (candidate.Detectable == CurrentTarget)
                {
                    var agentPos = Agent.transform.position;
                    distanceBetween = Vector3.Distance(candidate.RawPosition, agentPos);

                    if (distanceBetween > stoppingDistance && Agent.IsMoving)
                    {
                        CurrentPriority = candidate.Awareness < AwarenessToStopChase ? 
                            0 : (CurrentPriority += PriorityBuildRate * Time.deltaTime);
                    }
                    return;
                }
            }

            // clear our current target
            CurrentTarget = null;
        }
        
        // acquire a new target if possible
        foreach (var candidate in Sensors.ActiveTargets.Values)
        {
            // found a target to acquire
            if (candidate.Awareness >= MinAwarenessToChase)
            {
                CurrentTarget = candidate.Detectable;
                CurrentPriority = ChargePriority;
                return;
            }
        }*/
    }

    public override void OnGoalActivated(Action_Base _linkedAction)
    {
        base.OnGoalActivated(_linkedAction);
        blocking = true;
        
        CurrentPriority = ChargePriority;
    }

    public override void OnGoalDeactivated()
    {
        base.OnGoalDeactivated();
        CurrentTarget = null;
    }
    
    public override int CalculatePriority()
    {
        return Mathf.FloorToInt(CurrentPriority);
    }

    public override bool CanRun()
    {/*
        // no targets
        if (Sensors.ActiveTargets == null || Sensors.ActiveTargets.Count == 0)
            return false;

        // check if we have anything we are aware of
        foreach(var candidate in Sensors.ActiveTargets.Values)
        {
            var agentPos = Agent.transform.position;
            distanceBetween = Vector3.Distance(candidate.RawPosition, agentPos);

            if (candidate.Awareness >= MinAwarenessToChase 
                && distanceBetween >= stoppingDistance
                && !blocking)
            {
                return true;
            }
        }
*/
        return false;
    }
}
