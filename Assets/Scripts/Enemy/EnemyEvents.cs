using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyEvents : MonoBehaviour
{
    private Rigidbody body;
    private Animator animator;
    private NavMeshAgent agent;

    private Collider agentCollider;
    private Collider[] agentRagColliders;

    public float enemyHitpoints;
    public static float enemyManapoints;
    public static bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agentCollider = GetComponent<Collider>();
        agentRagColliders = GetComponentsInChildren<Collider>(true);
    }

    void Update()
    {
        if (enemyHitpoints < 1)
        {
            Death();
        }
    }

    // Collision Events - Damage and Player Items
    private void OnCollisionEnter(Collision collision)
    {
        body.isKinematic = false;

        // Environment
        if (collision.gameObject.tag == "Fire")
        {
            enemyHitpoints -= 1;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        body.isKinematic = true;
    }

    // Enemy Death
    private void Death()
    {
        if (agent.isOnNavMesh == true)
        {
            agent.velocity = Vector3.zero;
            agent.enabled = false;
        }

        animator.enabled = false;
        body.isKinematic = false;

        GameEvents.killCount++;
        Destroy(gameObject, 0);
    }
}
