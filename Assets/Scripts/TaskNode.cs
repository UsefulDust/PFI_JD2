using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public enum TaskState { Running, Success, Failure}
public abstract class TaskBT
{
    public abstract TaskState Execute();
}
public class TaskNode : Node
{
    protected List<TaskBT> Tasks { get; private set; } = new();
    private int CurrentTaskIndex { get; set; }

    public TaskNode(string tag, IEnumerable<TaskBT> tasks) : base(tag)
    {
        foreach (var task in tasks)
        {
            Tasks.Add(task);
        }
    }
    public void AddTask(TaskBT task) => Tasks.Add(task);

    protected override NodeState InnerEvaluate()
    {
        bool executeNextTask = true;
        int taskCount = Tasks.Count;

        while (executeNextTask)
        {
            TaskBT currentTask = Tasks[CurrentTaskIndex];
            TaskState currentTaskState = currentTask.Execute();

            switch (currentTaskState)
            {
                case TaskState.Failure:
                    State = NodeState.Failure;
                    return State;
                case TaskState.Running:
                    executeNextTask = false;
                    break;
                case TaskState.Success:
                    if (CurrentTaskIndex == taskCount - 1)
                    {
                        CurrentTaskIndex = 0;
                        State = NodeState.Success;
                        return State;
                    }
                    ++CurrentTaskIndex;
                    break;
            }
        }

        State = NodeState.Running;
        return State;
    }
}

public class Wait : TaskBT
{
    float ElapsedTime { get; set; }
    float SecondsToWait { get; set; }

    public Wait(float secondsToWait) => SecondsToWait = secondsToWait;

    // on tient pour acquis qu'Execute va être appeler à chaque frame
    public override TaskState Execute()
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime > SecondsToWait)
        {
            ElapsedTime = 0;
            return TaskState.Success;
        }

        return TaskState.Running;
    }
}
public class Patrol : TaskBT
{
    Vector3[] Destinations { get; set; }
    NavMeshAgent Agent { get; set; }
    int CurrentDestinationID { get; set; }
    public Patrol(Vector3[] destinations, NavMeshAgent agent)
    {
        Destinations = destinations;
        Agent = agent;
    }
    public override TaskState Execute()
    {
        Vector3 currentDestination = Destinations[CurrentDestinationID];

        Agent.destination = currentDestination;

        if (Vector3.Distance(currentDestination, Agent.transform.position) <= Agent.stoppingDistance)
        {
            CurrentDestinationID = (CurrentDestinationID + 1) % Destinations.Length;
            return TaskState.Success;
        }
        return TaskState.Running;
    }
}
public class Follow : TaskBT
{
    GameObject gameobjectToFollow { get; set; }

    GameObject Agent { get; set; }

    public Follow(GameObject player, GameObject agent)
    {
        gameobjectToFollow = player;
        Agent = agent;
    }

    public override TaskState Execute()
    {
        if (Vector3.Distance(gameobjectToFollow.transform.position, Agent.transform.position) <= 1.5f)
        {
            return TaskState.Success;
        }
        Agent.transform.position = Vector3.MoveTowards(Agent.transform.position, gameobjectToFollow.transform.position - new Vector3(0, 1, 0), 2 * Time.deltaTime);
        return TaskState.Running;
    }
}
public class Punch : TaskBT
{
    public override TaskState Execute()
    {
        Debug.Log("punch");
        return TaskState.Success;
    }
}