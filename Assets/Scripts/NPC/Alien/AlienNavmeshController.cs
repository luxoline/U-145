using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienNavmeshController : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;
    public bool canMove = false;
    [SerializeField] float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.destination == null) return;

        if (!canMove){
            navMeshAgent.isStopped = true;
            animator.SetBool("isRunning", false);
            var lookPos = navMeshAgent.destination;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
            return;
        }

        var dest = GameObject.FindGameObjectWithTag("Player").transform.position;
        dest.y = transform.position.y;
        var dist = Vector3.Distance(transform.position, dest);

        if (dist <= maxDistance)
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("isRunning", false);
        }
        else
        {
            navMeshAgent.isStopped = false;
            animator.SetBool("isRunning", true);
            SetDestination(dest);
        }
    }


    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
        Debug.Log("SetDestination");
    }
}
