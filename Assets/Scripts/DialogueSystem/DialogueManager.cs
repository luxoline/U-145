﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] bool playSoundOnText;
    public GameObject dialogueCanvas;
    public TMP_Text whoIsTalkingText;
    public TMP_Text dialogueText;
    public Button[] optionButtons;
    public float typeSpeed;
    public PlayerController playerController;
    [SerializeField] AudioClip[] textSounds;
    [SerializeField] float minSoundWait, maxSoundWait;
    [SerializeField] bool waitBetweenSounds;
    float soudWaitCtr;
    AudioSource audioSource;

    [SerializeField] float disabledR, disabledG, disabledB;
    [SerializeField] float enabledR, enabledG, enabledB;

    private DialogueData currentDialogue;
    bool skipDialogue, isDialogueStarted, isNpc;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SingletonThisGameObject();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isDialogueStarted)
        {
            skipDialogue = true;
        }
    }

    private void SingletonThisGameObject()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartDialogue(DialogueData dialogueData, bool isNpc = false)
    {
        if (dialogueData == null) return;
        if(dialogueCanvas.activeSelf) return;
        GameObject.Find("FreeLook Camera").GetComponent<Cinemachine.CinemachineFreeLook>().enabled = false;
        this.isNpc = isNpc;
        playerController.animator.SetBool("pass", false);
        playerController.canWalk = false;
        isDialogueStarted = true;
        dialogueCanvas.SetActive(true);
        currentDialogue = dialogueData;
        //QuestManager.Instance.DisableQuestCanvas();
        if(isNpc) InteractionCanvasManager.Instance.DisableCanvas();
        UpdateDialogUI();
    }

    public void OnOptionSelected(int optionIndex)
    {
        Debug.Log("optionIndex: " + optionIndex);
        if (currentDialogue != null && optionIndex < currentDialogue.options.Length)
        {
            DialogOption selectedOption = currentDialogue.options[optionIndex];
            Debug.Log("optionIndex2: " + optionIndex);
            var analyticsResult = Analytics.CustomEvent("SelectedOption", new Dictionary<string, object>
            {
                { "Selected", selectedOption.text }
            });

            Debug.Log($"Analytics result: {analyticsResult}");
            if (selectedOption.quest != null)
            {
                QuestManager.Instance.SetQuest(selectedOption.quest);
            }

            if (selectedOption.nextDialogue != null)
            {
                Debug.Log("optionIndex3: " + optionIndex);
                currentDialogue = selectedOption.nextDialogue;
                UpdateDialogUI();
                isDialogueStarted = true;
                return;
            }
            else if (selectedOption.isEndingOption)
            {
                GameObject.Find("FreeLook Camera").GetComponent<Cinemachine.CinemachineFreeLook>().enabled = true;
                dialogueCanvas.SetActive(false);
                playerController.canWalk = true;
                isDialogueStarted = false;
                QuestManager.Instance.questCanvas.SetActive(true);
                WaypointManager.Instance.EnableCanvas();
                var csCam = GameObject.FindGameObjectWithTag("CSCamera");
                if (csCam != null) csCam.SetActive(false);
                playerController.animator.SetBool("pass", true);
                if(isNpc) InteractionCanvasManager.Instance.EnableCanvas();
            }
            var goName = selectedOption.onclickGameObjectName;
            if (goName != "0")
            {
                Debug.Log("goName: " + goName);
                GameObject.Find(goName).transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }

    private void UpdateDialogUI()
    {
        whoIsTalkingText.text = currentDialogue.whoIsTalking;
        foreach (var btn in optionButtons)
        {
            btn.gameObject.SetActive(false);
        }
        
        StartCoroutine(TypeDialogueText());
    }

    private IEnumerator TypeDialogueText()
    {
        soudWaitCtr = 0;
        dialogueText.text = string.Empty;

        float typeSpeed = 0.05f;
        float elapsedTime = 0f;

        bool showAllText = false;

        foreach (char letter in currentDialogue.dialogueText)
        {
            dialogueText.text += letter;
            if (playSoundOnText) PlayTextSound();

            elapsedTime += Time.deltaTime;

            if (skipDialogue || elapsedTime >= typeSpeed * currentDialogue.dialogueText.Length)
            {
                showAllText = true;
                skipDialogue = false;
                break;
            }

            yield return new WaitForSeconds(typeSpeed);
        }

        if (showAllText)
        {
            dialogueText.text = currentDialogue.dialogueText;
        }

        ShowOptionButtons();
        isDialogueStarted = false;
    }

    private void PlayTextSound()
    {
        if (currentDialogue.whoIsTalking == "cocuk") return;


        var clip = textSounds[UnityEngine.Random.Range(0, textSounds.Length)];

        if (waitBetweenSounds)
        {
            soudWaitCtr -= Time.deltaTime;

            if (soudWaitCtr <= 0)
            {
                soudWaitCtr = UnityEngine.Random.Range(minSoundWait, maxSoundWait);
                audioSource.PlayOneShot(clip);
            }
            return;
        }

        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(clip);
        audioSource.volume = 1f;
    }


    private void ShowOptionButtons()
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < currentDialogue.options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = currentDialogue.options[i].text;
                if (currentDialogue.options[i].isDisabled){
                    optionButtons[i].interactable = false;
                    var colors = optionButtons[i].colors;
                    colors.normalColor = new Color(disabledR, disabledG, disabledB);
                    optionButtons[i].colors = colors;
                }
                else{
                    optionButtons[i].interactable = true;
                    var colors = optionButtons[i].colors;
                    colors.normalColor = new Color(enabledR, enabledG, enabledB);
                    optionButtons[i].colors = colors;
                }
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
