using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class EnemyBossManager : MonoBehaviour
    {
        
        public string bossName;
        
        [Header("Second Phase FX")]
        public GameObject particleFX;

        UIBossHealthBar bossHealthBar;
        EnemyManagerGoap _enemyManagerGoap;
        EnemyStats enemyStats;
        EnemyAnimatorManager _enemyAnimatorManager;

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
            _enemyManagerGoap = FindObjectOfType<EnemyManagerGoap>();
            enemyStats = GetComponent<EnemyStats>();
            _enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        }

        private void Start()
        {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
        }

        public void UpdateBossHealthBar(int currentHealth, int maxHealth)
        {
            bossHealthBar.SetBossCurrentHealth(currentHealth);
            
            if (currentHealth <= maxHealth / 4 && _enemyManagerGoap.canDoPhaseShift)
            {
                ShiftToSecondPhase();
                _enemyManagerGoap.canDoPhaseShift = false;
            }
        }

        public void ShiftToSecondPhase()
        {
            enemyStats.currentHealth = enemyStats.maxHealth;
            bossHealthBar.SetBossCurrentHealth(enemyStats.maxHealth);
        }
    }
}