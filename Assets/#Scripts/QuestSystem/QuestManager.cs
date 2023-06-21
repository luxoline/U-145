using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public GameObject questCanvas;

    public TMP_Text questText;

    public Quest currentQuest;
    public List<string> questObjects = new List<string>();
    public List<int> completedQuests = new List<int>();

    void Awake()
    {
        SingletonThisGameObject();
        if (currentQuest == null)
        {
            currentQuest = Resources.Load<Quest>("Quests/0");
            Debug.Log(currentQuest.questDescription);
            SetQuestText();
        }
    }

    private void SingletonThisGameObject()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsCurrentQuest(int num)
    {
        return currentQuest.questNumber == num;
    }

    public bool IsCurrentQuest(Quest quest)
    {
        return currentQuest == quest;
    }

    public void SetQuestText()
    {
        questText.text = "Current Quest:\n" + currentQuest.questDescription;
    }

    public void SetQuest()
    {
        if (Resources.Load<Quest>("Quests/" + (currentQuest.questNumber + 1)) == null)
        {
            Debug.Log("All quests completed");
            return;
        }
        currentQuest = Resources.Load<Quest>("Quests/" + (currentQuest.questNumber + 1));
        SetQuestText();
    }

    public bool CheckQuestObjects()
    {
        if (currentQuest.neededItemTag == "" || currentQuest.neededItemTag == null)
        {
            return true;
        }
        return questObjects.Contains(currentQuest.neededItemTag);
    }
    public void CompleteCurrentQuest()
    {
        completedQuests.Add(currentQuest.questNumber);
    }
}
