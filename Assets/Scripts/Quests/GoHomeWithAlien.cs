using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoHomeWithAlien : MonoBehaviour
{
    public int questNumber = 1;
    bool dialogueStarted = false;

    [SerializeField] GameObject mainCamera, dialogueCamera, player, alien;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!QuestManager.Instance.IsCurrentQuest(questNumber)) return;
            if (alien.GetComponent<NavMeshAgent>().isStopped && !dialogueStarted)
            {
                ChangeCameras();
                StartDialogue();
                dialogueStarted = true;
            }
        }
    }

    private void ChangeCameras()
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.canWalk = false;
        playerController.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerController.GetComponent<Animator>().SetTrigger("idle");

        var playerLookPos = alien.transform.position;
        playerLookPos.y = playerController.transform.position.y;
        playerController.transform.LookAt(playerLookPos);

        var alienLookPos = player.transform.position;
        alienLookPos.y = alien.transform.position.y;
        alien.transform.LookAt(alienLookPos);

        var alienNavmeshAgent = alien.GetComponent<NavMeshAgent>();
        var alienNavmeshController = alien.GetComponent<AlienNavmeshController>();
        alienNavmeshController.canMove = false;
        alienNavmeshAgent.SetDestination(transform.position);

        WaypointManager.Instance.DisableCanvas();
        QuestManager.Instance.DisableQuestCanvas();
        mainCamera.SetActive(false);
        dialogueCamera.SetActive(true);
    }

    private void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/GoHomeWithAlien/0"));
    }
}
