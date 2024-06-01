using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class GOAPPlanner : MonoBehaviour
    {
        Goal_Base[] Goals;
        Action_Base[] Actions;

        [SerializeField] Goal_Base ActiveGoal;
        [SerializeField] Action_Base ActiveAction;

        [SerializeField] private Goal_Base bestGoal = null;
        [SerializeField] private Action_Base bestAction = null;

        void Awake()
        {
            //TODO: change to getComponentsInChildren
            Goals = FindObjectsOfType<Goal_Base>();
            Actions = FindObjectsOfType<Action_Base>();
        }

        public void UpdateGoapPlanner()
        {
            // find the highest priority goal that can be activated
            foreach (var goal in Goals)
            {
                // first tick the goal
                goal.OnTickGoal();

                // can it run?
                if (!goal.CanRun())
                    continue;

                // is it a worse priority?
                if (!(bestGoal == null || goal.CalculatePriority() > bestGoal.CalculatePriority()))
                    continue;

                // find the best cost action
                Action_Base candidateAction = null;
                foreach (var action in Actions)
                {
                    if (!action.GetSupportedGoals().Contains(goal.GetType()))
                        continue;

                    // found a suitable action
                    if (candidateAction == null || action.GetCost() < candidateAction.GetCost())
                        candidateAction = action;
                }

                // did we find an action?
                if (candidateAction != null)
                {
                    bestGoal = goal;
                    bestAction = candidateAction;
                }
            }

            // if no current goal
            if (ActiveGoal == null)
            {
                ActiveGoal = bestGoal;
                ActiveAction = bestAction;

                if (ActiveGoal != null)
                    ActiveGoal.OnGoalActivated(ActiveAction);
                if (ActiveAction != null)
                    ActiveAction.OnActivated(ActiveGoal);
            } // no change in goal?
            else if (ActiveGoal == bestGoal)
            {
                // action changed?
                if (ActiveAction != bestAction)
                {
                    ActiveAction.OnDeactivated();

                    ActiveAction = bestAction;

                    ActiveAction.OnActivated(ActiveGoal);
                }
            } // new goal or no valid goal
            else if (ActiveGoal != bestGoal)
            {
                ActiveGoal.OnGoalDeactivated();
                ActiveAction.OnDeactivated();

                ActiveGoal = bestGoal;
                ActiveAction = bestAction;

                if (ActiveGoal != null)
                    ActiveGoal.OnGoalActivated(ActiveAction);
                if (ActiveAction != null)
                    ActiveAction.OnActivated(ActiveGoal);
            }

            // tick the action
            if (ActiveAction != null)
                ActiveAction.OnTick();
        }
    }
}