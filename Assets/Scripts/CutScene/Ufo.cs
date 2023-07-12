using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField] GameObject alien, ufoPrefab;

    private void Start()
    {
        InteractionCanvasManager.Instance.DisableCanvas();
        WaypointManager.Instance.DisableCanvas();
    }
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
