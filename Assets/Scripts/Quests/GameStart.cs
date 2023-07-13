using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject ufo, player;
    void Start()
    {
        InteractionCanvasManager.Instance.DisableCanvas();
        WaypointManager.Instance.DisableCanvas();
        QuestManager.Instance.DisableQuestCanvas();
    }

    public void UfoFall()
    {
        //var dd = Resources.Load<DialogueData>("Dialogues/GameStart/0");
        //DialogueManager.Instance.StartDialogue(dd);
        ufo.SetActive(true);
        player.GetComponent<Animator>().SetBool("turn", true);
        player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("idle");
    }
}
