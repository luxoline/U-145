using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Data", menuName = "Dialogue", order = 2)]
public class DialogueData : ScriptableObject
{
    public string whoIsTalking;
    public string dialogueText;
    public DialogOption[] options;
}

[System.Serializable]
public class DialogOption
{
    public string text;
    public DialogueData nextDialogue;
    public Quest quest;
    public bool isEndingOption;
    public bool isDisabled;
    public string onclickGameObjectName;
}
