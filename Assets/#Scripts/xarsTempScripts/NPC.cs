using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    bool canTalk = false;
    public int npcQuestNumber = 1;


    // Update is called once per frame
    void Update()
    {

        if (canTalk && Input.GetKeyDown(KeyCode.E))
        {
            if (QuestManager.Instance.IsCurrentQuest(npcQuestNumber))
            {
                if (QuestManager.Instance.CheckQuestObjects())
                {
                    Debug.Log("npc gorevi tamamlandi");
                    QuestManager.Instance.SetQuest();
                    Debug.Log(QuestManager.Instance.currentQuest.questName + " gorevi aktif");
                }
                else
                {
                    Debug.Log("gorev aktif ama tamamlanmadi, gorev hatirlatmasi diyalogu yazilabilir: " + QuestManager.Instance.currentQuest.questDescription);
                }
            }
            else if(QuestManager.Instance.IsCurrentQuest(npcQuestNumber - 1))
            {
                Debug.Log("npc ile konus gorevi tamamlandi");
                QuestManager.Instance.SetQuest();
                Debug.Log(QuestManager.Instance.currentQuest.questName + " gorevi aktif");
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
