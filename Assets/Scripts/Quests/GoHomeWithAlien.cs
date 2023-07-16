using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoHomeWithAlien : MonoBehaviour
{
    public int questNumber = 1;
    bool dialogueStarted = false, subtitleStarted = false;

    [SerializeField] GameObject dialogueCamera, player, alien;

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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (subtitleStarted) return;
            if (QuestManager.Instance.IsCurrentQuest(2))
            {
                StartCoroutine(StartSubtitles());
                subtitleStarted = true;
            }
        }
    }

    IEnumerator StartSubtitles()
    {
        yield return new WaitForSeconds(4f);
        var dd = Resources.LoadAll<DialogueData>("Subtitles/OdunAl");
        foreach (var d in dd)
        {
            Debug.Log(d.dialogueText);
            SubtitleManager.Instance.StartSubtitle(d.dialogueText);
            yield return new WaitForSeconds(d.dialogueText.Length * SubtitleManager.Instance.subtitleTime);
        }
        SubtitleManager.Instance.DisableCanvas();
    }

    private void ChangeCameras()
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.canWalk = false;
        playerController.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerController.GetComponentInChildren<Animator>().SetTrigger("idle");

        var playerLookPos = alien.transform.position;
        playerLookPos.y = playerController.transform.position.y;
        playerController.transform.LookAt(playerLookPos);
        playerController.transform.GetChild(0).LookAt(playerLookPos);

        var alienLookPos = player.transform.position;
        alienLookPos.y = alien.transform.position.y;
        alien.transform.LookAt(alienLookPos);

        var alienNavmeshAgent = alien.GetComponent<NavMeshAgent>();
        var alienNavmeshController = alien.GetComponent<AlienNavmeshController>();
        alienNavmeshController.canMove = false;
        alienNavmeshAgent.SetDestination(transform.position);

        WaypointManager.Instance.DisableCanvas();
        QuestManager.Instance.DisableQuestCanvas();
        dialogueCamera.SetActive(true);
    }

    private void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/GoHomeWithAlien/0"));
    }
}
