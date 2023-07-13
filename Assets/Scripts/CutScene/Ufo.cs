using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField] GameObject alien, ufoPrefab;
    [SerializeField] GameObject mainCamera, ufoFallCutSceneCamera;

    private void Start()
    {
        mainCamera.SetActive(false);
        ufoFallCutSceneCamera.SetActive(true);
        InteractionCanvasManager.Instance.DisableCanvas();
        WaypointManager.Instance.DisableCanvas();
        QuestManager.Instance.DisableQuestCanvas();
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
