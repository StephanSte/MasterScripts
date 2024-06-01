using Sirenix.OdinInspector;
using ST;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * THIS WILL BE REPLACED BY AN IMPLEMENTATION OF IHEALTH
 */

public class HealthSystem : IHealth
{
    [SerializeField]
    private int health = 50;

    [SerializeField] 
    private int maxHealth = 50;

    [SerializeField] public bool bossDead = false;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private bool player;
    
    public void Start()
    {
        health = maxHealth;
    }

    [Button]
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (player)
        {
            text.text = health.ToString();
        }
        
        if (health <= 0)
        {
            if (player)
            {
                text.text = "0";
            }
            Death();
        }
    }

    public override void Death()
    {
        if (player)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (GetComponent<GOAPPlanner>())
        {
            //enable you win screen
            bossDead = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(int amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
    }
}