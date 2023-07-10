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
        PathPoints = new Transform[Path.transform.childCount];
        for(int i =0; i<PathPoints.Length; i++) PathPoints[i] = Path.transform.GetChild(i);
    }

    void Update()
    {
        MoveNpc();
        Debug.Log(index);
        Debug.Log(Vector3.Distance(transform.position, PathPoints[index].transform.position));
    }

    void MoveNpc()
    {
        if (Vector3.Distance(transform.position, PathPoints[index].transform.position) < minDistance)
        {
             if (index >= 0 && index < PathPoints.Length) index++; 
             else index = 0;
        } 
        agent.SetDestination(PathPoints[index].position);
        animator.SetFloat("vertical", 1);
    }
}
