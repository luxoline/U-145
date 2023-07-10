using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public GameObject Path;
    public Transform[] PathPoints;

    public float minDistance = 1;
    public int index = 0;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  
        animator = GetComponent<Animator>();    
        //PathPoints = new Transform[Path.transform.childCount];
        //for(int i =0; i<PathPoints.Length; i++) PathPoints[i] = Path.transform.GetChild(i);
    }

    void Update()
    {
        MoveNpc();
        /*Debug.Log((int)transform.position.x == (int)PathPoints[PathPoints.Length - 1].transform.position.x);
        Debug.Log("Kadýn: "+ transform.position.x);
        Debug.Log("Nokta: "+ PathPoints[PathPoints.Length - 1].transform.position.x);*/
        Debug.Log(Vector3.Distance(transform.position, PathPoints[index].transform.position));

    }

    void MoveNpc()
    {
        if (!agent.isStopped)
        {
            if (Vector3.Distance(transform.position, PathPoints[index].transform.position) < minDistance)
            {
                if (index >= 0 && index < PathPoints.Length - 1) index = (index + 1) % PathPoints.Length; 
                agent.SetDestination(PathPoints[index].position);
            }
            animator.SetFloat("vertical", 1);
            if (transform.position.x.ToString("F2") ==PathPoints[PathPoints.Length - 1].transform.position.x.ToString("F2"))
            {
                agent.isStopped = true;
            }
        }
       else animator.SetFloat("vertical", 0);

    }
}
