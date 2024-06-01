using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ST
{
    public class Action_Attack : Action_Base
    {
        List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] {typeof(Goal_Attack)});

        Goal_Attack _attackGoal;
        
        public EnemyAttackAction currentAttack;
        public EnemyAttackAction[] enemyAttacks;
        bool willDoComboOnNextAttack;
        public bool hasPerformedAttack;
        public bool ranomAttackDamage;

        protected float verticalMovementValue = 0;
        protected float horizontalMovementValue = 0;

        [Header("Second Phase Attacks")] public bool hasPhaseShifted;
        public EnemyAttackAction[] secondPhaseAttacks;
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

            DecideOnAttack();

            if (willDoComboOnNextAttack && enemyManager.canDoCombo)
            {
                AttackTargetWithCombo();
            }

            if (!hasPerformedAttack)
            {
                AttackTarget();
                RollForComboChance();
            }
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

        private void AttackTarget()
        {
            if (!currentAttack || currentAttack.actionAnimation == null) return;
            if (ranomAttackDamage)
            {
                EnemyWeaponSlotManager enemyWeaponSlotManager = FindObjectOfType<EnemyWeaponSlotManager>();
                var rightHandDamageCollider = enemyWeaponSlotManager.rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                rightHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
                rightHandDamageCollider.currentWeaponDamage = Random.Range(1, 100);
            }
            enemyAnimationManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            //Audio audio = GetComponentInChildren<Audio>();
            //audio.playAttackBossAudio();
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
            hasPerformedAttack = true;
            
        }

        private void AttackTargetWithCombo()
        {
            willDoComboOnNextAttack = false;
            enemyAnimationManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
            currentAttack = null;
        }

        private void RollForComboChance()
        {
            float comboChance = Random.Range(0, 100);

            if (enemyManager.allowAIToPerformCombos && comboChance <= enemyManager.comboLikelyHood)
            {
                if (currentAttack && currentAttack.comboAction != null)
                {
                    willDoComboOnNextAttack = true;
                    currentAttack = currentAttack.comboAction;
                }
                else
                {
                    willDoComboOnNextAttack = false;
                    currentAttack = null;
                }
            }
        }
        
        
        public void DecideOnAttack()
        {
            enemyAnimationManager.anim.SetFloat("Vertical", verticalMovementValue, 0.1f, Time.deltaTime);
            enemyAnimationManager.anim.SetFloat("Horizontal", horizontalMovementValue, 0.1f, Time.deltaTime);
            hasPerformedAttack = false;

            if (enemyManager.isInteracting)
            {
                enemyAnimationManager.anim.SetFloat("Vertical", 0);
                enemyAnimationManager.anim.SetFloat("Horizontal", 0);
                return;
            }

            HandleRotateTowardsTarget();
            GetNewAttack();
        }
        
        protected void HandleRotateTowardsTarget()
        {
            //Rotate manually
            if (enemyManager.isPreformingAction)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                    enemyManager.rotationSpeed / Time.deltaTime);
            }
            //Rotate with pathfinding (navmesh)
            else
            {
                NavMeshAgent navmeshAgent = FindObjectOfType<NavMeshAgent>();
                navmeshAgent.enabled = true;
                //characterAgent.MoveTo(enemyManager.currentTarget.transform.position);
                Vector3 test = enemyManager.currentTarget.transform.position;
                navmeshAgent.SetDestination(test);

                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation,
                    navmeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
        }

        void GetNewAttack()
        {
            if (hasPhaseShifted && enemyManager.canDoPhaseShift)
            {
                GetNewSecondPhaseAttack();
            }
            else
            {
                GetNewNormalAttack();
            }
        }
        
        protected virtual void GetNewNormalAttack()
        {
            var tempval = Random.Range(0, enemyAttacks.Length);
            currentAttack = enemyAttacks[tempval];

            /*
            Vector3 targetsDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position,
                enemyManager.transform.position);

            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }


            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        if (currentAttack != null)
                            return;

                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                        {
                            currentAttack = enemyAttackAction;
                        }
                    }
                }
            }*/
        }

        protected virtual void GetNewSecondPhaseAttack()
        {
             Vector3 targetsDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
                float viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);
                float distanceFromTarget =
                    Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

                int maxScore = 0;

                for (int i = 0; i < secondPhaseAttacks.Length; i++)
                {
                    EnemyAttackAction enemyAttackAction = secondPhaseAttacks[i];

                    if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                        && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                    {
                        if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                            && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                        {
                            maxScore += enemyAttackAction.attackScore;
                        }
                    }
                }


                int randomValue = Random.Range(0, maxScore);
                int temporaryScore = 0;

                for (int i = 0; i < secondPhaseAttacks.Length; i++)
                {
                    EnemyAttackAction enemyAttackAction = secondPhaseAttacks[i];

                    if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                        && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                    {
                        if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                            && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                        {
                            if (currentAttack != null)
                                return;

                            temporaryScore += enemyAttackAction.attackScore;

                            if (temporaryScore > randomValue)
                            {
                                currentAttack = enemyAttackAction;
                            }
                        }
                    }
                }
        }
    }
}