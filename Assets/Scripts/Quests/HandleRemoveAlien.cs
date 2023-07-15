using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRemoveAlien : MonoBehaviour
{
    [SerializeField] GameObject alien;
    private void OnEnable()
    {
        alien.SetActive(false);
    }
}
