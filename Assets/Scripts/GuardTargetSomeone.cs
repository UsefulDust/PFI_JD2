using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GuardTargetSomeone : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    NavMeshAgent navMeshAgent;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        StartCoroutine(UpdateTargetPosition());
    }

    IEnumerator UpdateTargetPosition()
    {
        while (true)
        {
            navMeshAgent.destination = targetTransform.position;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
