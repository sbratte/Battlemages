using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    private float stopDistance = 6f;

    public void ChasePlayer(GameObject player, float playerDistance, NavMeshAgent agent)
    {
        if (agent.isOnNavMesh && playerDistance >= stopDistance)
        {
            agent.destination = player.transform.position;
        } 
        else if (agent.isOnNavMesh)
        {
            agent.destination = transform.position;
        }
    }
}
