using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReturnHome : MonoBehaviour
{
    public int[] questNumbers;
    bool dialogueStarted = false;

    [SerializeField] GameObject dialogueCamera, player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var isCurrentQuest = QuestManager.Instance.IsCurrentQuest(questNumbers[0]) || QuestManager.Instance.IsCurrentQuest(questNumbers[1]);
            if (!isCurrentQuest) return;

            if (!dialogueStarted)
            {
                StartDialogue();
                dialogueStarted = true;
            }
        }
    }

    private void StartDialogue()
    {
        ChangeCameras();
        DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/ReturnHome/0"));
    }


    private void ChangeCameras()
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.canWalk = false;
        playerController.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerController.GetComponentInChildren<Animator>().SetTrigger("idle");

        var playerLookPos = transform.position;
        playerLookPos.y = playerController.transform.position.y;
        playerController.transform.LookAt(playerLookPos);

        WaypointManager.Instance.DisableCanvas();
        QuestManager.Instance.DisableQuestCanvas();
        dialogueCamera.SetActive(true);
    }
}
