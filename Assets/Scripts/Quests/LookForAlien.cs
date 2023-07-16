using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForAlien : MonoBehaviour
{
    [SerializeField] GameObject tempPlayer, cutsceneCamera;
    [SerializeField] int questNumber;
    private void OnTriggerEnter(Collider other)
    {
        if (QuestManager.Instance.IsCurrentQuest(questNumber))
        {
            if (other.CompareTag("Player"))
            {
                QuestManager.Instance.DisableQuestCanvas();
                WaypointManager.Instance.DisableCanvas();
                InteractionCanvasManager.Instance.DisableCanvas();
                other.GetComponent<PlayerController>().canWalk = false;
                tempPlayer.SetActive(true);
                cutsceneCamera.SetActive(true);
                StartCoroutine(ActivateCam());
            }
        }
    }

    IEnumerator ActivateCam()
    {
        yield return new WaitForSeconds(1f);
        cutsceneCamera.GetComponent<Animator>().SetBool("pass", true);
    }

    
}
