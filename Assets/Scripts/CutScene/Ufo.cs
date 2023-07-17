using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField] GameObject alien, ufoPrefab, tempPlayer, freeLook;
    [SerializeField] GameObject ufoFallCutSceneCamera;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip ufoFallSound;
    private void Start()
    {
        //mainCamera.SetActive(false);
        ufoFallCutSceneCamera.SetActive(true);
        InteractionCanvasManager.Instance.DisableCanvas();
        WaypointManager.Instance.DisableCanvas();
        QuestManager.Instance.DisableQuestCanvas();
    }

    void FinishUfoFallCutScene()
    {
        alien.SetActive(true);
        ufoPrefab.SetActive(true);
        DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/UFOFallCutScene/0"));
        ufoFallCutSceneCamera.SetActive(false);
        tempPlayer.SetActive(false);
        //freeLook.GetComponent<Cinemachine.CinemachineFreeLook>().enabled = true;
    }

    public void PlayFallSound()
    {
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(ufoFallSound);
    }

}
