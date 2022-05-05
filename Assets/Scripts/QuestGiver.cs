using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public List<Quest> quest;

    [System.NonSerialized]
    public Player player;

    public GameObject questWindow;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI goldText;

    private int numberQuestCompleted = 0;


    private void Update()
    {
        quest[numberQuestCompleted].isUnlocked = true;
    }

    public void OpenQuestWindow()
    {
        if (numberQuestCompleted < quest.Count)
        {
            questWindow.SetActive(true);

            titleText.text = quest[numberQuestCompleted].title;
            descriptionText.text = quest[numberQuestCompleted].description;
            goldText.text = quest[numberQuestCompleted].goldReward.ToString();

        }
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }

    public void AcceptQuest()
    {
        CloseQuestWindow();

        quest[numberQuestCompleted].isActive = true;
        player.quest = quest[numberQuestCompleted];
    }

    public void questCompleted()
    {
        numberQuestCompleted += 1;
    }
}
