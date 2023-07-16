using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Interaction : MonoBehaviour
{

    bool canTalk = false;
    public int npcQuestNumber = 1;
    private GameObject npc;
    public bool isTalked = false;
    private void Start()
    {
        this.npc = transform.parent.gameObject;
    }

    void Update()
    {
        //Debug.Log(npc.GetComponent<Animator>().GetFloat("vertical"));

        if (canTalk && Input.GetKeyDown(KeyCode.E))
        {
            if (QuestManager.Instance.IsCurrentQuest(npcQuestNumber))
            {
                if (isTalked)
                {
                    WaypointManager.Instance.DisableCanvas();
                    DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/" + transform.parent.gameObject.name + "/NoQuest/0"), true);
                    return;
                }
                DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/" + transform.parent.gameObject.name + "/QuestActive/0"), true);
            }
            else
            {
                DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/" + transform.parent.gameObject.name + "/NoQuest/0"), true);
            }
            isTalked = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = true;
            InteractionCanvasManager.Instance.EnableCanvas();
            InteractionCanvasManager.Instance.SetTarget(transform);

            if (WaypointManager.Instance.target == this.transform.parent)
            {
                WaypointManager.Instance.DisableCanvas();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            WaypointManager.Instance.EnableCanvas();
            canTalk = false;
            InteractionCanvasManager.Instance.DisableCanvas();
        }
    }
}
