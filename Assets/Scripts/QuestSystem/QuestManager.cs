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

    public GameObject[] deneme;

    void Start()
    {
        SingletonThisGameObject();
        DisableQuestCanvas();
        //var firstQuest = Resources.Load<Quest>("Quests/0");
        //SetQuest(firstQuest);
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
        questText.text = "G�rev:\n" + currentQuest.questDescription + "\n" + currentQuest.questDescription2;
    }

    public void CompleteQuest()
    {
        Debug.Log("Quest completed " + currentQuest.questNumber);

        if (Resources.Load<Quest>("Quests/" + ((int)currentQuest.questNumber + 1).ToString()) == null)
        {
            Debug.Log("All quests completed");
            return;
        }
        int num = currentQuest.questNumber + 1;
        Debug.Log("Quest next " + num);
        if (currentQuest.goNextQuestAutomatically) currentQuest = Resources.Load<Quest>("Quests/" + num.ToString());
        WaypointManager.Instance.SetTarget(GameObject.Find(currentQuest.questOwnerGameObjectName).transform);
        SetQuestText();
    }

    public void SetQuest(Quest quest)
    {
        currentQuest = quest;
        WaypointManager.Instance.DisableCanvas();
        WaypointManager.Instance.SetTarget(GameObject.Find(currentQuest.questOwnerGameObjectName).transform);
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

    public void DisableQuestCanvas()
    {
        questCanvas.SetActive(false);
    }
}
