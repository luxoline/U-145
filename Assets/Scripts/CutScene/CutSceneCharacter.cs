using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCharacter : MonoBehaviour
{
    [SerializeField] Camera mainCamera, ufoFallCutSceneCamera;

    [SerializeField] GameObject ufo;
    void CheckCornQuest()
    {
        if (QuestManager.Instance.currentQuest.questOwnerGameObjectName == "Corn3")
        {
            mainCamera.gameObject.SetActive(false);
            ufoFallCutSceneCamera.gameObject.SetActive(true);
            WaypointManager.Instance.DisableCanvas();
            InteractionCanvasManager.Instance.DisableCanvas();
            QuestManager.Instance.DisableQuestCanvas();
            ufo.SetActive(true);
        }

        //GameObject.Find(QuestManager.Instance.currentQuest.questOwnerGameObjectName).GetComponent<WateringCorn>().doneWatering = true;
        GameObject.Find(QuestManager.Instance.currentQuest.questOwnerGameObjectName).GetComponent<WateringCorn>().DoneWatering(GetComponent<PlayerController>());

    }
}
