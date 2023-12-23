using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public Sequence(string tag) : base(tag) { }
    public Sequence(string tag, IEnumerable<Node> children) : base(tag, children) { }
    protected override NodeState InnerEvaluate()
    {
        foreach (var currentChild in Children)
        {
            NodeState childState = currentChild.Evaluate();
            switch (childState)
            {
                case NodeState.Failure:
                    State = NodeState.Failure;
                    return State;
                case NodeState.Running:
                    State = NodeState.Running;
                    return State;
            }
        }
        State = NodeState.Success;
        return State;
    }
}
