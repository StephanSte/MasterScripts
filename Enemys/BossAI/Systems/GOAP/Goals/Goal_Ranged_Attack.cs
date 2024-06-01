using UnityEngine;

public class Goal_Ranged_Attack : Goal_Base
{
    [SerializeField] int AttackPriority = 0;
    [SerializeField] float MinAwarenessToAttack = 1f;
    [SerializeField] float CurrentAwareness = 0f;
    [SerializeField] private float minDistanceToAttack = 5f;
    [SerializeField] float distanceBetween = 0;
    DetectableTarget CurrentTarget;
    [SerializeField] float CurrentPriority = 0;
    [SerializeField] float PriorityBuildRate = 2f;

    public Vector3 MoveTarget => CurrentTarget != null ? CurrentTarget.transform.position : transform.position;

    public override void OnTickGoal()
    {/*
        CurrentPriority += PriorityBuildRate * Time.deltaTime;
        
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

                    if (distanceBetween >= minDistanceToAttack)
                    {
                        
                        if (CurrentPriority > 100)
                        {
                            CurrentPriority = 100;
                        }
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
            if (candidate.Awareness >= MinAwarenessToAttack)
            {
                CurrentTarget = candidate.Detectable;
                CurrentPriority = AttackPriority;
                return;
            }
        }*/
    }

    public override void OnGoalActivated(Action_Base _linkedAction)
    {
        base.OnGoalActivated(_linkedAction);
        
        CurrentPriority = AttackPriority;
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

            CurrentAwareness = candidate.Awareness;
            
            if (candidate.Awareness >= MinAwarenessToAttack
                && distanceBetween >= minDistanceToAttack
                && !blocking)
            {
                return true;
            }
        }*/
        
        return false;
    }
}
