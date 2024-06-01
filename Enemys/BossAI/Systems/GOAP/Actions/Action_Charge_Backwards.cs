using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Charge_Backwards : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] {typeof(Goal_Charge_Backwards)});

    Goal_Attack _attackGoal;
    //[SerializeField] private DamageTrigger damageTrigger;

    [SerializeField] private float ChargeSpeed = -55f;
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

        Animator anim = GetComponent<Animator>();
        
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayOneShot(MeleeAudio);

       //Vector3 location = damageTrigger.getPositionToChargeAwayFrom(chargeDistance);

        //Agent.MoveTo(location, ChargeSpeed, ChargeAcceleration);

        anim.Play("BossArmature_jump_back");
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();

        _attackGoal = null;
    }

    public override void OnTick()
    {
        OnActivated(LinkedGoal);
    }
}