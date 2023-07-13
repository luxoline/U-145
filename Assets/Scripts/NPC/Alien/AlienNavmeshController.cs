using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienNavmeshController : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    public bool canMove = false;
    [SerializeField] float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.destination == null) return;

        if (!canMove){
            navMeshAgent.isStopped = true;
            return;
        }

        var dest = GameObject.FindGameObjectWithTag("Player").transform.position;
        dest.y = transform.position.y;
        var dist = Vector3.Distance(transform.position, dest);
        Debug.Log(GameObject.FindGameObjectWithTag("Player").name);
        if (dist <= maxDistance)
        {
            navMeshAgent.isStopped = true;
        }
        else
        {
            navMeshAgent.isStopped = false;
            SetDestination(dest);
        }
    }


    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
        Debug.Log("SetDestination");
    }
}
