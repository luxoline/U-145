using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField] GameObject alien, ufoPrefab;
    void AlienFall()
    {
        alien.SetActive(true);
    }

    void FinishUfoFallCutScene()
    {
        ufoPrefab.SetActive(true);
        Destroy(gameObject);
    }
}
