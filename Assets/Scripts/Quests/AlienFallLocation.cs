using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFallLocation : MonoBehaviour
{
    [SerializeField] GameObject mainCamera, dialogueCamera;

    [SerializeField] int questNumber;
    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();

        if (other.CompareTag("Player"))
        {
            if (!QuestManager.Instance.IsCurrentQuest(questNumber)) return;

            playerController.canWalk = false;
            playerController.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerController.GetComponent<Animator>().SetTrigger("idle");
            var lookPos = transform.position;
            lookPos.y = playerController.transform.position.y;
            playerController.transform.LookAt(lookPos);
            DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/AlienFallLocation/0"));
            WaypointManager.Instance.DisableCanvas();
            QuestManager.Instance.DisableQuestCanvas();
            mainCamera.SetActive(false);
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
