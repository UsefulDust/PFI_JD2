using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehaviour : MonoBehaviour
{
    [SerializeField] GameObject Player;

    NavMeshAgent Agent;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        TaskBT[] task0 = new TaskBT[]
        {
            new Follow(Player, Agent)
        };
        TaskBT[] task1 = new TaskBT[]
        {

        };
        TaskNode followNode = new TaskNode("followNode0", task0);
        // punch

        Node seq1 = new Sequence("seq0", new[] { followNode });
    }
}
