using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public GameObject dialogueCanvas;
    public TMP_Text whoIsTalkingText;
    public TMP_Text dialogueText;
    public Button[] optionButtons;
    public float typeSpeed;
    public PlayerController playerController;
    [SerializeField] AudioClip[] textSounds;
    [SerializeField] float minSoundWait, maxSoundWait;
    float soudWaitCtr;
    AudioSource audioSource;

    private DialogueData currentDialogue;
    bool skipDialogue, isDialogueStarted;

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

    public void StartDialogue(DialogueData dialogueData)
    {
        playerController.canWalk = false;
        isDialogueStarted = true;
        dialogueCanvas.SetActive(true);
        currentDialogue = dialogueData;
        //QuestManager.Instance.DisableQuestCanvas();
        //InteractionCanvasManager.Instance.gameObject.SetActive(false);
        UpdateDialogUI();
    }

    public void OnOptionSelected(int optionIndex)
    {
        if (currentDialogue != null && optionIndex < currentDialogue.options.Length)
        {
            DialogOption selectedOption = currentDialogue.options[optionIndex];

            
            if (selectedOption.quest != null)
            {
                QuestManager.Instance.SetQuest(selectedOption.quest);
            }

            if (selectedOption.nextDialogue != null)
            {
                StartDialogue(selectedOption.nextDialogue);
            }
            else if (selectedOption.isEndingOption)
            {
                dialogueCanvas.SetActive(false);
                playerController.canWalk = true;
                isDialogueStarted = false;
                QuestManager.Instance.questCanvas.SetActive(true);
                WaypointManager.Instance.EnableCanvas();
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
            if (currentDialogue.whoIsTalking != "Cocuk") PlayTextSound();

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
        soudWaitCtr -= Time.deltaTime;

        if (soudWaitCtr <= 0)
        {
            soudWaitCtr = UnityEngine.Random.Range(minSoundWait, maxSoundWait);
            var clip = textSounds[UnityEngine.Random.Range(0, textSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    private void ShowOptionButtons()
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < currentDialogue.options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = currentDialogue.options[i].text;
                if (currentDialogue.options[i].isDisabled) optionButtons[i].interactable = false;
                else optionButtons[i].interactable = true;
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
