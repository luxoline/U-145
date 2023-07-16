using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFallLocation : MonoBehaviour
{
    [SerializeField] GameObject dialogueCamera, alien;

    [SerializeField] int questNumber;
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {
            if (!QuestManager.Instance.IsCurrentQuest(questNumber)) return;
            var playerController = other.GetComponent<PlayerController>();
            playerController.canWalk = false;
            playerController.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerController.GetComponentInChildren<Animator>().SetTrigger("idle");
            var lookPos = alien.transform.position;
            lookPos.y = playerController.transform.position.y;
            playerController.transform.LookAt(lookPos);
            alien.GetComponent<Animator>().SetBool("standUp", true);
            DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/deneme/0"));
            WaypointManager.Instance.DisableCanvas();
            QuestManager.Instance.DisableQuestCanvas();
            dialogueCamera.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (QuestManager.Instance.IsCurrentQuest(questNumber)) return;
            gameObject.SetActive(false);
        }
    }
}
