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
            npc.GetComponent<Animator>().SetFloat("vertical", 1);
            isTalked = true;
            if (QuestManager.Instance.IsCurrentQuest(npcQuestNumber))
            {
                DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/" + transform.parent.gameObject.name + "/QuestActive/0"), true);
                if (isTalked)
                {
                    DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/" + transform.parent.gameObject.name + "/NoQuest/0"), true);
                }
            }
            else
            {
                DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/" + transform.parent.gameObject.name + "/NoQuest/0"), true);
            }
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
                WaypointManager.Instance.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = false;
            InteractionCanvasManager.Instance.DisableCanvas();

            if (WaypointManager.Instance.target == this.transform.parent)
            {
                WaypointManager.Instance.gameObject.SetActive(true);
            }
        }
    }
}
