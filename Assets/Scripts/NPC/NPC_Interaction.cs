using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Interaction : MonoBehaviour
{

    bool canTalk = false;
    public int npcQuestNumber = 1;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (canTalk && Input.GetKeyDown(KeyCode.E))
        {
            if (QuestManager.Instance.IsCurrentQuest(npcQuestNumber))
            {
                if (QuestManager.Instance.CheckQuestObjects())
                {
                    DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/" + transform.parent.gameObject.name + "/QuestCompleted/0"));
                }
                else
                {
                    DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/"+ transform.parent.gameObject.name + "/QuestActive/0"));
                }
            }
            else if(QuestManager.Instance.IsCurrentQuest(npcQuestNumber - 1))
            {
                DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/" + transform.parent.gameObject.name + "/GetQuest/0"));
                Debug.Log(transform.parent.gameObject.name);
            }
            else
            {
                DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/" + transform.parent.gameObject.name + "/NoQuest/0"));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = true;
            InteractionCanvasManager.Instance.gameObject.SetActive(true);
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
            InteractionCanvasManager.Instance.gameObject.SetActive(false);

            if (WaypointManager.Instance.target == this.transform.parent)
            {
                WaypointManager.Instance.gameObject.SetActive(true);
            }
        }
    }
}
