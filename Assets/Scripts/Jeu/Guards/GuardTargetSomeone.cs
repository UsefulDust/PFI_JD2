using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum Cible { Joueur, Adversaire, LesDeux, Aucune }
public class GuardTargetSomeone : MonoBehaviour
{
    [SerializeField] Transform targetPlayerTransform;
    [SerializeField] Transform targetOpponentTransform;
    [SerializeField] float runningSpeed = 8;
    [SerializeField] CollisionDetection collisionScript;
    [SerializeField] CharacterController characterController;
    [SerializeField] PlayerMovement playerMovement;
    float defaultSpeed;
    NavMeshAgent navMeshAgent;
    Animator animator;
    public bool endGame = false;

    public Cible cibleEnDanger = Cible.Aucune;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        defaultSpeed = navMeshAgent.speed;
    }

    void Start()
    {   
        StartCoroutine(UpdateTargetPosition());
    }

    public Vector3 SetCible()
    {
        
        if (cibleEnDanger == Cible.LesDeux)
        {
            animator.SetBool("NeedToRun", true);
            navMeshAgent.speed = runningSpeed;
            if (Vector3.Distance(navMeshAgent.transform.position, targetOpponentTransform.position) < Vector3.Distance(navMeshAgent.transform.position, targetPlayerTransform.position))
                return targetOpponentTransform.position;
            else
                return targetPlayerTransform.position;
        }
        else if (cibleEnDanger == Cible.Joueur)
        {
            animator.SetBool("NeedToRun", true);
            navMeshAgent.speed = runningSpeed;
            return targetPlayerTransform.position;
        }
        else if (cibleEnDanger == Cible.Adversaire)
        {
            animator.SetBool("NeedToRun", true);
            navMeshAgent.speed = runningSpeed;
            return targetOpponentTransform.position;
        }
        else
        {
            animator.SetBool("NeedToRun", false);
            return new Vector3();
        }

    }

    IEnumerator UpdateTargetPosition()
    {
        while (true)
        {
            if (navMeshAgent.isActiveAndEnabled)
                navMeshAgent.destination = SetCible();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, targetPlayerTransform.position) < 2.35f && !endGame)
        {
            playerMovement.deactivateMovement();
            playerMovement.enabled = false;
            //characterController.enabled = false;
            transform.LookAt(targetPlayerTransform, Vector3.up);
            //transform.rotation = Quaternion.identity;
            Camera.main.fieldOfView = 110;
            Camera.main.transform.LookAt(gameObject.transform, Vector3.up);
            collisionScript.Collision(gameObject.name, targetPlayerTransform.tag, navMeshAgent);
            endGame = true;
        }
        if (Vector3.Distance(gameObject.transform.position, targetOpponentTransform.position) < 2.35f && !endGame)
        {
            transform.LookAt(targetOpponentTransform, Vector3.up);
            //transform.rotation = Quaternion.identity;
            collisionScript.Collision(gameObject.name, targetOpponentTransform.tag, navMeshAgent);
            endGame = true;
        }
    }
}
