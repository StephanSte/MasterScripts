using System;
using UnityEngine;
public class WinScreen : MonoBehaviour
{
    [SerializeField] private HealthSystem bosshealth;
    [SerializeField] private GameObject endscreen;
    
    private bool skip = false;
    public void FixedUpdate()
    {
        if (!skip && bosshealth.bossDead)
        {
            endscreen.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            skip = true;
        }
    }

}
