using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class PlayerStats : CharacterStats
    {
        PlayerManager playerManager;

        HealthBar healthBar;
        StaminaBar staminaBar;
        FocusPointBar focusPointsBar;
        PlayerAnimatorManager animatorHandler;
        TestManager _testManager;

        public float staminaRegenerationAmount = 1;
        public float staminaRegenTimer = 0;

        private void Awake()
        {
            _testManager = FindObjectOfType<TestManager>();

            playerManager = GetComponent<PlayerManager>();

            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            focusPointsBar = FindObjectOfType<FocusPointBar>();
            animatorHandler = GetComponentInChildren<PlayerAnimatorManager>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);

            maxFocusPoints = SetMaxFocusPointsFromFocusLevel();
            currentFocusPoints = maxFocusPoints;
            //focusPointsBar.SetMaxFocusPoints(maxFocusPoints);
            //focusPointsBar.SetCurrentFocusPoints(currentFocusPoints);
        }

        public override void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer = poiseResetTimer - Time.deltaTime;
            }
            else if (poiseResetTimer <= 0 && !playerManager.isInteracting)
            {
                totalPoiseDefence = armorPoiseBonus;
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        private float SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        private float SetMaxFocusPointsFromFocusLevel()
        {
            maxFocusPoints = focusLevel * 10;
            return maxFocusPoints;
        }

        public override void TakeDamage(int damage, string damageAnimation = "Damage_01")
        {
            if (playerManager.isInvulnerable)
                return;

            base.TakeDamage(damage, damageAnimation = "Damage_01");
            healthBar.SetCurrentHealth(currentHealth);
            animatorHandler.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0)
            {
                animatorHandler.PlayTargetAnimation("Dead_01", true);
                TestParameter parameter = FindObjectOfType<TestParameter>();
                parameter.deaths[parameter.getId()] = parameter.deaths[parameter.getId()] + 1;
                _testManager.restartTest();
            }
        }

        public void TakeDamageNoAnimation(int damage)
        {
            if (playerManager.isInvulnerable)
                return;
            
            string damageAnimation = "Empty";
            base.TakeDamage(damage, damageAnimation);
            healthBar.SetCurrentHealth(currentHealth);
            if (currentHealth <= 0)
            {
                animatorHandler.PlayTargetAnimation("Dead_01", true);
                TestParameter parameter = FindObjectOfType<TestParameter>();
                parameter.deaths[parameter.getId()] = parameter.deaths[parameter.getId()] + 1;
                _testManager.restartTest();
            }
        }

        public void TakeStaminaDamage(int damage)
        {
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
        }

        public void RegenerateStamina()
        {
            if (playerManager.isInteracting)
            {
                staminaRegenTimer = 0;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;

                if (currentStamina < maxStamina && staminaRegenTimer > 1f)
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }

        public void HealPlayer(int healAmount)
        {
            currentHealth = currentHealth + healAmount;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            healthBar.SetCurrentHealth(currentHealth);
        }

        public void DeductFocusPoints(int focusPoints)
        {
            currentFocusPoints = currentFocusPoints - focusPoints;

            if (currentFocusPoints < 0)
            {
                currentFocusPoints = 0;
            }

            focusPointsBar.SetCurrentFocusPoints(currentFocusPoints);
        }

        public void AddSouls(int souls)
        {
            soulCount = soulCount + souls;
        }
    }
}