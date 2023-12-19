using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// https://youtu.be/gcT6NmN3Zyo?si=0i40tjDlreUa1Mlu
[RequireComponent(typeof(NavMeshAgent))]
public class GuardPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    [SerializeField] float speed = 4;
    NavMeshAgent agent;
    int currentPatrolPointIndex = 0;

    float plusProche = 100000000;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        AllerPlusProchePointPatrouille();
    }

    public void AllerPlusProchePointPatrouille()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            var distance = Vector3.Distance(transform.position, patrolPoints[i].position);
            if (distance < plusProche)
            {
                currentPatrolPointIndex = i;
                plusProche = distance;
            }
        }
    }

    void Update()
    {
        agent.speed = speed;
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) >= 0.3f)
        {
            if (agent.isActiveAndEnabled)
                agent.SetDestination(patrolPoints[currentPatrolPointIndex].position);
        }
        else 
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
    }
}
