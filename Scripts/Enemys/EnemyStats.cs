using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class EnemyStats : CharacterStats
    {
        EnemyManagerGoap enemyManagerGoap; //refactor so only one type
        EnemyAnimationManager enemyAnimationManager;
        EnemyBossManager enemyBossManager;
        TestManager _testManager;
        public UIEnemyHealthBar enemyHealthBar;
        public int soulsAwardedOnDeath = 50;

        public bool isBoss;

        private void Awake()
        {
            _testManager = FindObjectOfType<TestManager>();
            enemyManagerGoap = GetComponent<EnemyManagerGoap>();
            enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
            enemyBossManager = GetComponent<EnemyBossManager>();
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        void Start()
        {
            if (!isBoss)
            {
                enemyHealthBar.SetMaxHealth(maxHealth);
            }
        }
        
        public override void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer = poiseResetTimer - Time.deltaTime;
            }
            else if (poiseResetTimer <= 0 && enemyManagerGoap != null && !enemyManagerGoap.isInteracting)
            {
                totalPoiseDefence = armorPoiseBonus;
            }
            else if (poiseResetTimer <= 0 && enemyManagerGoap != null && !enemyManagerGoap.isInteracting)
            {
                totalPoiseDefence = armorPoiseBonus;
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamageNoAnimation(int damage)
        {
            currentHealth = currentHealth - damage;

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if (isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        public void BreakGuard()
        {
            enemyAnimationManager.PlayTargetAnimation("Break Guard", true);
        }

        public override void TakeDamage(int damage, string damageAnimation = "Damage_01")
        {

            base.TakeDamage(damage, damageAnimation = "Damage_01");

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if (isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            enemyAnimationManager.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            currentHealth = 0;
            enemyAnimationManager.PlayTargetAnimation("Dead_01", true);
            isDead = true;
            _testManager.endTest();
        }
    }
}