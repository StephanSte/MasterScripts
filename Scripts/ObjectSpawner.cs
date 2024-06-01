using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameSettings gameSettings;
    public GameObject entity;
    public float frequency = 5f;
    public bool spawnOnAwake;

    public bool limit;
    [ShowIf("limit")] 
    public float range = 5f;
    [ShowIf("limit")]
    public float count = 1f;
    [ShowIf("limit")]
    public LayerMask layerMask;

    private float cooldown;

    private void Awake()
    {
        if (spawnOnAwake && !gameSettings.peacefulMode)
        {
            SpawnEntity();
            cooldown = frequency;
        }
            

    }

    private void Update()
    {
        if (gameSettings.peacefulMode) return;
        bool cooldownHasNotExpired = cooldown > 0f;
        if (cooldownHasNotExpired)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        if (limit && Physics.OverlapSphere(transform.position, range, layerMask).Length >= count)
            return;

        cooldown = frequency;
        SpawnEntity();
    }

    private void SpawnEntity()
    {
        Instantiate(entity, transform.position, Quaternion.identity, transform);
    }
}
