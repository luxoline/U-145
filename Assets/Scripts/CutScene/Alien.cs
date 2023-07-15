using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] Camera mainCamera, ufoFallCutSceneCamera;

    [SerializeField] GameObject alienPrefab;

    [SerializeField] GameObject tempPlayer, mainPlayer;

    void FinishAlienFallCutScene()
    {
        var pos = transform.position;
        mainPlayer.SetActive(true);
        tempPlayer.SetActive(false);
        
        ufoFallCutSceneCamera.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        alienPrefab.SetActive(true);
        WaypointManager.Instance.DisableCanvas();
        DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/UFOFallCutScene/0"));
        QuestManager.Instance.questCanvas.gameObject.SetActive(true);
        QuestManager.Instance.questText.text = "";
        //Destroy(gameObject);
    }

}
