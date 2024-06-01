using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManagerGoap enemyManager;
        EnemyAnimationManager enemyAnimationManager;
        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockerCollider;

        public LayerMask detectionLayer;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManagerGoap>();
            enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
        }

        private void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true);
        }
    }
}