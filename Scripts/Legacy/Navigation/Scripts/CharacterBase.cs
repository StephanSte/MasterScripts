using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
public class CharacterBase : MonoBehaviour
{
    [SerializeField] EFaction _Faction;

    public EFaction Faction => _Faction;
}



using UnityEngine;
using System.Collections.Generic;

public class HTN : MonoBehaviour
{
    private List<Subtask> plan = new List<Subtask>();
    private Dictionary<string, Subtask> subtasks = new Dictionary<string, Subtask>();
    public Transform player;

    public void PlanForGoal(string goal)
    {
        plan.Clear();
        if (goal == "AttackPlayer")
        {
            plan.Add(subtasks["MoveTowardsPlayer"]);
            plan.Add(subtasks["AttackPlayer"]);
        }
    }

    public void ExecutePlan()
    {
        foreach (Subtask subtask in plan)
        {
            subtask.Execute();
        }
    }

    private void Start()
    {
        subtasks.Add("MoveTowardsPlayer", new MoveTowardsPlayer(this));
        subtasks.Add("AttackPlayer", new AttackPlayer(this));
    }
}

public class Subtask
{
    public string name;
    public bool isComplete = false;
    protected HTN htn;

    public Subtask(string name, HTN htn)
    {
        this.name = name;
        this.htn = htn;
    }

    public virtual void Execute()
    {
        isComplete = true;
    }
}

public class MoveTowardsPlayer : Subtask
{
    public MoveTowardsPlayer(HTN htn) : base("MoveTowardsPlayer", htn) { }

    public override void Execute()
    {
        if (htn.player != null)
        {
            // Move towards the player
            htn.transform.position = Vector3.MoveTowards(htn.transform.position, htn.player.position, Time.deltaTime);
        }
        else
        {
            isComplete = false;
        }
    }
}

public class AttackPlayer : Subtask
{
    public AttackPlayer(HTN htn) : base("AttackPlayer", htn) { }

    public override void Execute()
    {
        if (htn.player != null)
        {
            // Attack the player
            Debug.Log("Attacking player");
        }
        else
        {
            isComplete = false;
        }
    }
}


*/

//goap extension
/*
 *
 * using UnityEngine;
using System.Collections.Generic;

public class HTN : MonoBehaviour
{
    private List<Subtask> plan = new List<Subtask>();
    private Dictionary<string, Subtask> subtasks = new Dictionary<string, Subtask>();
    public Transform player;

    public void PlanForGoal(string goal)
    {
        plan.Clear();
        List<Subtask> currentPlan = new List<Subtask>();
        // code to use goal-oriented action planning to determine necessary subtasks
        // and their order of execution to achieve the given goal
        if(goal == "AttackPlayer")
        {
            plan = GOAPPlanner(goal,currentPlan);
        }
    }

    public void ExecutePlan()
    {
        foreach (Subtask subtask in plan)
        {
            subtask.Execute();
        }
    }

    private void Start()
    {
        subtasks.Add("MoveTowardsPlayer", new MoveTowardsPlayer(this));
        subtasks.Add("AttackPlayer", new AttackPlayer(this));
    }
    
    public List<Subtask> GOAPPlanner(string goal, List<Subtask> currentPlan)
    {
        // code for goal-oriented action planning algorithm
        // to determine necessary subtasks and their order of execution 
        // based on the current state of the world and the desired goal
        if (player != null)
        {
            currentPlan.Add(subtasks["MoveTowardsPlayer"]);
            currentPlan.Add(subtasks["AttackPlayer"]);
        }
        return currentPlan;
    }
}

 */
 
