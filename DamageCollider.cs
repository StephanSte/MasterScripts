using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager characterManager;
        Collider damageCollider;
        public bool enabledDamageColliderOnStartUp = false;

        [Header("Poise")] public float poiseBreak;
        public float offensivePoiseBonus;

        [Header("Damage")] public int currentWeaponDamage = 25;

        private TestParameter _testParameter;
        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = enabledDamageColliderOnStartUp;
            _testParameter = FindObjectOfType<TestParameter>();
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisaleDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
                HandlePlayer(collision);

            if (collision.tag == "Enemy" || collision.tag == "Target")
                HandleEnemy(collision);
        }

        private void HandlePlayer(Collider collision)
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
            BlockingCollider shield = collision.transform.GetComponentInChildren<BlockingCollider>();

            if (enemyCharacterManager != null)
            {
                if (enemyCharacterManager.isParrying)
                {
                    characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Stagger", true);
                    return;
                }

                if (shield != null && enemyCharacterManager.isBlocking)
                {
                    float physicalDamageAfterBlock =
                        currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsorption) / 100;

                    if (playerStats != null)
                    {
                        playerStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block Guard");
                        _testParameter.damageTotal[_testParameter.getId()] += Mathf.RoundToInt(physicalDamageAfterBlock);
                        return;
                    }
                }
            }

            if (playerStats != null)
            {
                //Debug.Log("Player's Poise is currently " + playerStats.totalPoiseDefence);
                playerStats.poiseResetTimer = playerStats.totalPoiseResetTime;
                playerStats.totalPoiseDefence = playerStats.totalPoiseDefence - poiseBreak;

                if (playerStats.totalPoiseDefence > poiseBreak)
                {
                    playerStats.TakeDamageNoAnimation(currentWeaponDamage);
                    _testParameter.damageTotal[_testParameter.getId()] += Mathf.RoundToInt(currentWeaponDamage);
                }
                else
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                    _testParameter.damageTotal[_testParameter.getId()] += Mathf.RoundToInt(currentWeaponDamage);
                }
            }
        }

        private void HandleEnemy(Collider collision)
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
            BlockingCollider shield = collision.transform.GetComponentInChildren<BlockingCollider>();

            if (enemyCharacterManager != null)
            {
                if (enemyCharacterManager.isParrying)
                {
                    characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Stagger", true);
                    return;
                }

                if (shield != null && enemyCharacterManager.isBlocking)
                {
                    float physicalDamageAfterBlock =
                        currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsorption) / 100;

                    if (enemyStats != null)
                    {
                        enemyStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block Guard");
                        return;
                    }
                }
            }

            if (enemyStats != null)
            {
                enemyStats.poiseResetTimer = enemyStats.totalPoiseResetTime;
                enemyStats.totalPoiseDefence = enemyStats.totalPoiseDefence - poiseBreak;
                Debug.Log("Enemies's Poise is currently " + enemyStats.totalPoiseDefence);

                if (enemyStats.isBoss)
                {
                    if (enemyStats.totalPoiseDefence > poiseBreak)
                    {
                        enemyStats.TakeDamageNoAnimation(currentWeaponDamage);
                    }
                    else
                    {
                        enemyStats.TakeDamageNoAnimation(currentWeaponDamage);
                        enemyStats.BreakGuard();
                    }
                }
                else
                {
                    if (enemyStats.totalPoiseDefence > poiseBreak)
                    {
                        enemyStats.TakeDamageNoAnimation(currentWeaponDamage);
                    }
                    else
                    {
                        enemyStats.TakeDamage(currentWeaponDamage);
                    }
                }
            }
        }
    }
}