using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] Camera mainCamera, ufoFallCutSceneCamera;

    [SerializeField] GameObject alienPrefab;

    void FinishAlienFallCutScene()
    {
        mainCamera.gameObject.SetActive(true);
        ufoFallCutSceneCamera.gameObject.SetActive(false);
        alienPrefab.SetActive(true);
        WaypointManager.Instance.DisableCanvas();
        DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/UFOFallCutScene/0"));
        QuestManager.Instance.questCanvas.gameObject.SetActive(true);
        QuestManager.Instance.questText.text = "";
        //Destroy(gameObject);
    }
}
