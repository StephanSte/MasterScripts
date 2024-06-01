
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter(Collider other)
    {
        boss.SetActive(true);
    }
}