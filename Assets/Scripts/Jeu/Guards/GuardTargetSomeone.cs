using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum Cible { Joueur, Adversaire, LesDeux, Aucune }
public class GuardTargetSomeone : MonoBehaviour
{
    [SerializeField] Transform targetPlayerTransform;
    [SerializeField] Transform targetOpponentTransform;
    float defaultSpeed;
    NavMeshAgent navMeshAgent;

    public Cible cibleEnDanger = Cible.Aucune;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        defaultSpeed = navMeshAgent.speed;
    }

    void Start()
    {
        
        StartCoroutine(UpdateTargetPosition());
    }

    public Vector3 SetCible()
    {
        navMeshAgent.speed = 11;
        if (cibleEnDanger == Cible.LesDeux)
        {
            if (Vector3.Distance(navMeshAgent.transform.position, targetOpponentTransform.position) < Vector3.Distance(navMeshAgent.transform.position, targetPlayerTransform.position))
                return targetOpponentTransform.position;
            else
                return targetPlayerTransform.position;
        }
        else if (cibleEnDanger == Cible.Joueur)
        {
            return targetPlayerTransform.position;
        }
        else if (cibleEnDanger == Cible.Adversaire)
        {
            return targetOpponentTransform.position;
        }
        else
        {
            navMeshAgent.speed = defaultSpeed;
            return new Vector3();
        }

    }

    IEnumerator UpdateTargetPosition()
    {
        while (true)
        {
            navMeshAgent.destination = SetCible();
            yield return new WaitForSeconds(0.25f);
        }
    }
}
