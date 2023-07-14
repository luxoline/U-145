using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoFallCamera : MonoBehaviour
{
    [SerializeField] GameObject ufo;

    public void UfoFall()
    {
        ufo.SetActive(true);
    }
}
