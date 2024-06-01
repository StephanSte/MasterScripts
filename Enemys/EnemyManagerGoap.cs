using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ST
{
    public class EnemyManagerGoap : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimationManager enemyAnimationManager;
        EnemyStats enemyStats;
        GOAPPlanner _goapPlanner;

        //public State currentState;
        public CharacterStats currentTarget;
        public NavMeshAgent navmeshAgent;
        public Rigidbody enemyRigidBody;

        public bool isPreformingAction;
        public bool isInteracting;
        public float rotationSpeed = 15;
        public float maximumAmbushRadius = 2f;
        public float maximumAttackRadius = 2f;

        [Header("Combat Flags")]
        public bool canDoCombo;
        public bool canDoPhaseShift;

        [Header("A.I Settings")]
        public float detectionRadius = 20;
        //The higher, and lower, respectively these angles are, the greater detection FIELD OF VIEW (basically like eye sight)
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;
        public float currentRecoveryTime = 0;

        [Header("A.I Combat Settings")]
        public bool allowAIToPerformCombos;
        public bool isPhaseShifting;
        public float comboLikelyHood;

        private void Awake()
        {
            currentTarget = FindObjectOfType<PlayerStats>();
            _goapPlanner = GetComponentInChildren<GOAPPlanner>();
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
            enemyStats = GetComponent<EnemyStats>();
            enemyRigidBody = GetComponent<Rigidbody>();
            navmeshAgent = GetComponentInChildren<NavMeshAgent>();
            navmeshAgent.enabled = false;
        }

        private void Start()
        {
            enemyRigidBody.isKinematic = false;
        }

        private void Update()
        {
            HandleRecoveryTimer();
            _goapPlanner.UpdateGoapPlanner();

            isRotatingWithRootMotion = enemyAnimationManager.anim.GetBool("isRotatingWithRootMotion");
            isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");
            isPhaseShifting = enemyAnimationManager.anim.GetBool("isPhaseShifting");
            isInvulnerable = enemyAnimationManager.anim.GetBool("isInvulnerable");
            canDoCombo = enemyAnimationManager.anim.GetBool("canDoCombo");
            canRotate = enemyAnimationManager.anim.GetBool("canRotate");
            enemyAnimationManager.anim.SetBool("isDead", enemyStats.isDead);
            
            //TODO: find better place to put this maybe locomotion manager
            RotateTowardsTargetWhilstAttacking();
        }

        private void LateUpdate()
        {
            navmeshAgent.transform.localPosition = Vector3.zero;
            navmeshAgent.transform.localRotation = Quaternion.identity;
        }

        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPreformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPreformingAction = false;
                }
            }
        }
        
        public void HandleRotateTowardsTarget()
        {
            //Rotate manually
            if (isPreformingAction)
            {
                Vector3 direction = currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,
                    rotationSpeed / Time.deltaTime);
            }
            //Rotate with pathfinding (navmesh)
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(navmeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyRigidBody.velocity;

                navmeshAgent.enabled = true;
                navmeshAgent.SetDestination(currentTarget.transform.position);
                enemyRigidBody.velocity = targetVelocity;
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    navmeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
            }
        }
        
        private void RotateTowardsTargetWhilstAttacking()
        {
            //Rotate manually
            if (canRotate && isInteracting)
            {
                Vector3 direction = currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                    rotationSpeed / Time.deltaTime);
            }
        }
    }
}
