using System;
using System.Collections;
using System.Collections.Generic;
using ST;
using UnityEngine;

public class SpellDamageCollider : DamageCollider
{
    public GameObject impactParticles;
    public GameObject projectileParticles;
    public GameObject muzzleParticles;

    private bool hasColided = false;
    private Vector3 impactNormal;

    private CharacterStats spellTarget;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        projectileParticles = Instantiate(projectileParticles, transform.position, transform.rotation);
        projectileParticles.transform.parent = transform;

        if (muzzleParticles)
        {
            muzzleParticles = Instantiate(muzzleParticles, transform.position, transform.rotation);
            Destroy(muzzleParticles, 2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasColided)
        {
            spellTarget = collision.transform.GetComponent<CharacterStats>();
            if (spellTarget != null)
            {
                spellTarget.TakeDamage(currentWeaponDamage);
            }
            hasColided = true;
            impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
            
            Destroy(projectileParticles);
            Destroy(impactParticles, 1f);
            Destroy(gameObject, 3f);
        }
    }
}
