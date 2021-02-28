using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public List<Transform> waypoints;

    private enum State
    {
        Wander,
        Chase,
        Attack,
        Retreat
    }

    private State state;

    private Wander wander;
    private Chase chase;
    private Attack attack;

    private GameObject player;
    private Animator animator;
    private NavMeshAgent agent;

    private float playerDistance;
    private float chaseRange = 15f;
    private float attackRange = 10f;
    private float rotationSpeed = 5f;
    private float timer = 0f;

    private Quaternion rotationToTarget;

    private void Awake()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        wander = GetComponent<Wander>();
        chase = GetComponent<Chase>();
        attack = GetComponent<Attack>();
        state = State.Wander;
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        timer += Time.deltaTime; // for enemy fire rate

        switch (state)
        {
            case State.Wander:
                wander.MoveToRandomWaypoint(waypoints, agent);
                TargetInChaseRange();
                break;
            case State.Chase:
                chase.ChasePlayer(player, playerDistance, agent);
                TargetInChaseRange();
                TargetInAttackRange();
                break;
            case State.Attack:
                if (timer > 2f)
                {
                    attack.AttackPlayer(player);
                    timer = 0;
                }

                TargetInChaseRange();
                break;
            default:
                state = State.Wander;
                break;
        }

        // Animation and Rotation
        if (agent.velocity.magnitude > 0.5f)
        {
            animator.SetFloat("MovementSpeed", 1);
        }
        else
        {
            animator.SetFloat("MovementSpeed", 0);
        }

        if (agent.velocity != Vector3.zero && state.ToString() == "Wander")
        {
            if (agent.velocity != Vector3.zero)
            {
                rotationToTarget = Quaternion.LookRotation(agent.velocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, (rotationSpeed * Time.deltaTime));
            }
        }
        else
        {
            Vector3 targetDirection = player.transform.position - transform.position;
            rotationToTarget = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, (rotationSpeed * Time.deltaTime));
        }
    }

    private void TargetInChaseRange()
    {
        if (playerDistance < chaseRange
            && playerDistance > attackRange)
        {
            state = State.Chase;
        }
        else if (playerDistance > chaseRange) 
        {
            state = State.Wander;
        }
    }

    private void TargetInAttackRange()
    {
        if (playerDistance < attackRange)
        {
            state = State.Attack;
        }
    }
}
