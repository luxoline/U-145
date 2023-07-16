using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEndGame : MonoBehaviour
{
    [SerializeField] GameObject fadeOut;
    private void OnEnable()
    {
        fadeOut.SetActive(true);
    }
}
