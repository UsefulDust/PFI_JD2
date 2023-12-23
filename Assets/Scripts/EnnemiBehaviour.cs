using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehaviour : MonoBehaviour
{
    [SerializeField] GameObject Player;

    Node root;
    void Start()
    {
        TaskBT[] task0 = new TaskBT[]
        {
            new Follow(Player, gameObject)
        };
        TaskBT[] task1 = new TaskBT[]
        {
            new Punch()
        };
        TaskNode followNode = new TaskNode("followNode0", task0);
        TaskNode punchNode = new TaskNode("punchNode0", task1);
        // punch

        Node seq0 = new Sequence("seq0", new[] { followNode, punchNode });

        root = seq0;
    }
    private void Update()
    {
        root.Evaluate();
    }
}
