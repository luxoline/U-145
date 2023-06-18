using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 1)]
public class Quest : ScriptableObject
{
    public string questName = "";
    public string questDescription = "";
    public int questNumber = 0;
    public string neededItemTag = null;
    public int neededItemAmount = 0;
    public Vector3 questLocation = Vector3.zero;

    public bool IsCurrentQuest(int questNumber)
    {
        return this.questNumber == questNumber;
    }

    public void SetQuestText()
    {
        QuestManager.Instance.questText.text = "Current Quest:\n" + questDescription;
    }


}