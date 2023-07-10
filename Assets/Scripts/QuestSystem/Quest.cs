using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 1)]
public class Quest : ScriptableObject
{
    public string questName = "";
    public string questDescription = "";
    public int questNumber = 0;
    public string neededItemTag = null;
    public int neededItemAmount = 0;
    public string questOwnerGameObjectName;
    public bool goNextQuestAutomatically;
}