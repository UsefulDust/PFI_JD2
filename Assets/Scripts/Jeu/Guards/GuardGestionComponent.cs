using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuardGestionComponent : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject opponent;

    [SerializeField] GuardTargetSomeone[] guardTargetSomeoneScripts;
    [SerializeField] GuardPatrol[] guardPatrolScripts;
    
    // 0 = joueur et 1 = adversaire
    string[] cible = new string[2];
    RaycastHit hit;
    void FixedUpdate()
    {
        if (Physics.Raycast(player.transform.position, Vector3.down, out hit, 25))
        {
            //Debug.Log("Joueur: " + hit.transform.tag);

            if (hit.transform.tag == "Gazon")
            {
                cible[0] = "Joueur";
            }
            else if (hit.transform.tag == "Arène")
            {
                cible[0] = null;

            }
        }

        if (Physics.Raycast(opponent.transform.position, Vector3.down, out hit, 25))
        {
            //Debug.Log("Adversaire: " + hit.transform.tag);

            if (hit.transform.tag == "Gazon")
            {
                cible[1] = "Adversaire";
            }
            else if (hit.transform.tag == "Arène")
            {
                cible[1] = null;
            }
        }

        if (cible.Contains("Adversaire") && cible.Contains("Joueur"))
        {
            foreach (var patrolScript in guardPatrolScripts)
            {
                patrolScript.enabled = false;
            }
            foreach (var targetSomeoneScript in guardTargetSomeoneScripts)
            {
                targetSomeoneScript.enabled = true;
                targetSomeoneScript.cibleEnDanger = Cible.LesDeux;
            }
        }
        else if (cible.Contains("Joueur"))
        {
            foreach (var patrolScript in guardPatrolScripts)
            {
                patrolScript.enabled = false;
            }
            foreach (var targetSomeoneScript in guardTargetSomeoneScripts)
            {
                targetSomeoneScript.enabled = true;
                targetSomeoneScript.cibleEnDanger = Cible.Joueur;
            }
        }
        else if (cible.Contains("Adversaire"))
        {
            foreach (var patrolScript in guardPatrolScripts)
            {
                patrolScript.enabled = false;
            }
            foreach (var targetSomeoneScript in guardTargetSomeoneScripts)
            {
                targetSomeoneScript.enabled = true;
                targetSomeoneScript.cibleEnDanger = Cible.Adversaire;
            }
        }
        else
        {
            foreach (var patrolScript in guardPatrolScripts)
            {
                patrolScript.enabled = true;
            }
            foreach (var targetSomeoneScript in guardTargetSomeoneScripts)
            {
                targetSomeoneScript.cibleEnDanger = Cible.Aucune;
                targetSomeoneScript.enabled = false;
            }
        }
    }
}
