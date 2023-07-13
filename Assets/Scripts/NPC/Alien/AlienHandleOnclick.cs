using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienHandleOnclick : MonoBehaviour
{
    private void OnEnable()
    {
        transform.parent.GetComponent<AlienNavmeshController>().canMove = true;
    }
}
