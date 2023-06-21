using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public GameObject dialogueCanvas;
    public TMP_Text dialogueText;
    public Button[] optionButtons;
    public float typeSpeed;
    private Coroutine typingCoroutine;

    private DialogueData currentDialogue;

    void Awake()
    {
        SingletonThisGameObject();
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
        dialogueCanvas.SetActive(true);
        currentDialogue = dialogueData;
        UpdateDialogUI();
    }

    public void OnOptionSelected(int optionIndex)
    {
        if (currentDialogue != null && optionIndex < currentDialogue.options.Length)
        {
            DialogOption selectedOption = currentDialogue.options[optionIndex];

            
            if (selectedOption.quest != null)
            {
                QuestManager.Instance.SetQuest();
            }

            if (selectedOption.nextDialogue != null)
            {
                StartDialogue(selectedOption.nextDialogue);
            }
            else if (selectedOption.isEndingOption)
            {
                dialogueCanvas.SetActive(false);
            }
        }

    }

    private void UpdateDialogUI()
    {
        foreach (var btn in optionButtons)
        {
            btn.gameObject.SetActive(false);
        }
        
        StartCoroutine(TypeDialogueText());
    }

    private IEnumerator TypeDialogueText()
    {
        dialogueText.text = string.Empty;

        float typeSpeed = 0.05f; // Yazma hızı
        float elapsedTime = 0f;

        bool showAllText = false;

        foreach (char letter in currentDialogue.dialogueText)
        {
            dialogueText.text += letter;

            elapsedTime += Time.deltaTime;

            // Eğer ekrana tıklanırsa veya yazma süresi sona ererse döngüyü bitirin
            if (Input.GetMouseButtonDown(0) || elapsedTime >= typeSpeed * currentDialogue.dialogueText.Length)
            {
                showAllText = true;
                break;
            }

            yield return new WaitForSeconds(typeSpeed);
        }

        // Eğer ekrana tıklandıysa veya yazma süresi sona erdiyse yazıyı tamamlayın
        if (showAllText)
        {
            dialogueText.text = currentDialogue.dialogueText; // Tüm yazıyı gösterin
        }

        ShowOptionButtons();
    }

    public void OnPanelClicked()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
            dialogueText.text = currentDialogue.dialogueText;
            ShowOptionButtons();
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
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
