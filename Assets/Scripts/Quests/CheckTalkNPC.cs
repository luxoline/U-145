using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTalkNPC : MonoBehaviour
{
    private void OnEnable()
    {
        Check();
    }

    private void Check()
    {
        var npcs = GameObject.FindGameObjectsWithTag("NPC");

        if (npcs != null)
        {
            foreach (var npc in npcs)
            {
                var sc = npc.GetComponent<NPC_Interaction>();
                if (!sc.isTalked)
                {
                    return;
                }
            }
            QuestManager.Instance.CompleteQuest();
        }
    }
}
