using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject ufoFallCamera;
    void Start()
    {
        InteractionCanvasManager.Instance.DisableCanvas();
        WaypointManager.Instance.DisableCanvas();
        //QuestManager.Instance.DisableQuestCanvas();
        StartCoroutine(StartSubtitles());
    }

    public void UfoFall()
    {
        ufoFallCamera.GetComponent<Animator>().SetBool("fall", true);
    }

    IEnumerator StartSubtitles()
    {
        var dd = Resources.LoadAll<DialogueData>("Subtitles/GameStart");
        foreach (var d in dd)
        {
            Debug.Log(d.dialogueText);
            SubtitleManager.Instance.StartSubtitle(d.dialogueText);
            yield return new WaitForSeconds(d.dialogueText.Length * SubtitleManager.Instance.subtitleTime);
        }
        SubtitleManager.Instance.DisableCanvas();
        UfoFall();
    }
}
