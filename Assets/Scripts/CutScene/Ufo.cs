using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField] GameObject alien, ufoPrefab, tempPlayer;
    [SerializeField] GameObject ufoFallCutSceneCamera;

    private void Start()
    {
        //mainCamera.SetActive(false);
        ufoFallCutSceneCamera.SetActive(true);
        InteractionCanvasManager.Instance.DisableCanvas();
        WaypointManager.Instance.DisableCanvas();
        QuestManager.Instance.DisableQuestCanvas();
    }

    void FinishUfoFallCutScene()
    {
        alien.SetActive(true);
        ufoPrefab.SetActive(true);
        DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/UFOFallCutScene/0"));
        ufoFallCutSceneCamera.SetActive(false);
        tempPlayer.SetActive(false);
    }

    

}
