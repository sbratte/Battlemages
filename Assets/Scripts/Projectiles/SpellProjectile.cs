using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    public GameObject spellExplosion;
    private Collider[] colliders;
    private Collider col;
    private float distanceTravelled;
    private float spellRange = 10f;
    private Vector3 lastPosition;
    
    private void Start()
    {
        col = GetComponent<Collider>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (distanceTravelled >= spellRange)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject != null)
        {
            Explode();
        }
    }

    private void Explode()
    {
        var explosion = Instantiate(spellExplosion, transform.position, Quaternion.identity) as GameObject;

        // add explosion force to the explosion prefab
        colliders = Physics.OverlapSphere(transform.position, 1.2f);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody body = nearbyObject.GetComponent<Rigidbody>();

            if (body != null)
            {
                body.AddExplosionForce(50f, transform.position, 1.2f);
            }
        }
        col.enabled = false;
        Destroy(explosion, 1f);
        Destroy(gameObject);
    }
}
