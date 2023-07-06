using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFallLocation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();

        if (other.CompareTag("Player"))
        {
            playerController.canWalk = false;
            DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/AlienFallLocation/0"));
            WaypointManager.Instance.gameObject.SetActive(false);
        }
    }
}
