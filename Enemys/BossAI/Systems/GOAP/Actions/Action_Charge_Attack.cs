using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Charge_Attack : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] {typeof(Goal_Charge_Attack)});

    Goal_Attack _attackGoal;

    [SerializeField] private float ChargeSpeed = 55f;
    [SerializeField] private float chargeDistance = 3f;
    [SerializeField] private float ChargeAcceleration = 30f;

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
/*
        Animator anim = GetComponent<Animator>();
        Audio audio = GetComponentInChildren<Audio>();
        DamageTrigger damageTrigger = GetComponentInChildren<DamageTrigger>();

        Vector3 location = damageTrigger.getPositionToChargeTo(chargeDistance);

        Agent.MoveTo(location, ChargeSpeed, ChargeAcceleration);

        audio.playAttackBossAudio();
        anim.Play("BossArmature|charge_attack");
        anim.SetBool("isAttackingCharge", true);
        damageTrigger.CreateDamageThingForSeconds();*/
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        Animator anim = GetComponent<Animator>();
        anim.SetBool("isAttackingCharge", false);
        _attackGoal = null;
    }

    public override void OnTick()
    {
        OnActivated(LinkedGoal);
    }
}