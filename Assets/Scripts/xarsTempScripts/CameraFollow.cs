using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float multiplier = 1f;
    Vector3 offset;
    [SerializeField] CinemachineFreeLook freeLook;

    private void Start()
    {
        offset = transform.position - target.position;
    }
    void Update()
    {
        // simple follow with lerp
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * multiplier);
    }

    private void OnEnable()
    {
        //StartCoroutine(ChangeCamera());
    }
}
