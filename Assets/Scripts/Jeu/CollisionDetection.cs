using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] Animator[] animatorTab;
    [SerializeField] GuardGestionComponent guardGestionComponent;
    [SerializeField] NavMeshAgent[] navMeshAgent;
    public void Collision(string guardName, string loserTag, NavMeshAgent agentGuard)
    {
        if (guardName == "Guard 1")
        {
            StartCoroutine(AnnoncerPerdantDansXSecondes(animatorTab[0], agentGuard, loserTag, navMeshAgent[0]));
        }
        else if (guardName == "Guard 2")
        {
            StartCoroutine(AnnoncerPerdantDansXSecondes(animatorTab[1], agentGuard, loserTag, navMeshAgent[1]));
        }
        else if (guardName == "Guard 3")
        {
            StartCoroutine(AnnoncerPerdantDansXSecondes(animatorTab[2], agentGuard, loserTag, navMeshAgent[2]));
        }
        else if (guardName == "Guard 4")
        {
            StartCoroutine(AnnoncerPerdantDansXSecondes(animatorTab[3], agentGuard, loserTag, navMeshAgent[3]));
        }
        Debug.Log(guardName);
    }

    IEnumerator AnnoncerPerdantDansXSecondes(Animator animator, NavMeshAgent agentGuard, string loserTag, NavMeshAgent navMeshAgent)
    {
        guardGestionComponent.victoireDétectée = true;
        navMeshAgent.enabled = false;
        agentGuard.speed = 0;
        // Désactiver les mouvements du joueur et tourner la caméra vers l'agent
        animator.SetBool("Punch", true);
        foreach (var animation in animatorTab)
        {
            if (animation != animator)
            {
                animation.SetBool("FriendCatch", true);
            }
        }
        yield return new WaitForSeconds(5);
        Perdant.AnnoncerPerdant(loserTag); 
    }
}
