using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
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
                    DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/" + gameObject.name + "/QuestCompleted/0"));
                }
                else
                {
                    DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/"+ gameObject.name + "/QuestActive/0"));
                }
            }
            else if(QuestManager.Instance.IsCurrentQuest(npcQuestNumber - 1))
            {
                Debug.Log("npc ile konus gorevi tamamlandi");
                DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/" + gameObject.name + "/GetQuest/0"));
            }
            else
            {
                Debug.Log("bu npcnin gorevi aktif dsegil, bos muhabbet yazilabilir");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = false;
        }
    }
}
