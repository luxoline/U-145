using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReturnHome : MonoBehaviour
{
    public int questNumber = 1;
    bool dialogueStarted = false;

    [SerializeField] GameObject mainCamera, dialogueCamera, player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!QuestManager.Instance.IsCurrentQuest(questNumber)) return;
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
        playerController.GetComponent<Animator>().SetTrigger("idle");

        var playerLookPos = transform.position;
        playerLookPos.y = playerController.transform.position.y;
        playerController.transform.LookAt(playerLookPos);

        WaypointManager.Instance.DisableCanvas();
        QuestManager.Instance.DisableQuestCanvas();
        mainCamera.SetActive(false);
        dialogueCamera.SetActive(true);
    }
}
