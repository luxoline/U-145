using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAblaOnclick : MonoBehaviour
{
    [SerializeField] GameObject abla;
    private void OnEnable()
    {
        abla.SetActive(true);
    }
}
