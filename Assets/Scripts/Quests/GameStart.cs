using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        InteractionCanvasManager.Instance.DisableCanvas();
        WaypointManager.Instance.DisableCanvas();
        var dd = Resources.Load<DialogueData>("Dialogues/GameStart/0");
        Debug.Log(dd.whoIsTalking);
        DialogueManager.Instance.StartDialogue(dd);
    }
}
