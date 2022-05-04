using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public List<Quest> quest;

    public Player player;

    public GameObject questWindow;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI goldText;

    private int numberQuestCompleted = 0;


    public void OpenQuestWindow()
    {
        if (numberQuestCompleted <= quest.Count)
        {
            questWindow.SetActive(true);

            titleText.text = quest[numberQuestCompleted].title;
            descriptionText.text = quest[numberQuestCompleted].description;
            goldText.text = quest[numberQuestCompleted].goldReward.ToString();

        }
        

        //if (!quest[0].isCompleted && !quest[0].isCanceled)
        //{
        //    titleText.text = quest[0].title;
        //    descriptionText.text = quest[0].description;
        //    goldText.text = quest[0].goldReward.ToString();
        //}
        //else if (!quest[1].isCompleted && !quest[1].isCanceled)
        //{
        //    titleText.text = quest[1].title;
        //    descriptionText.text = quest[1].description;
        //    goldText.text = quest[1].goldReward.ToString();
        //}
        //else if (!quest[2].isCompleted && !quest[2].isCanceled)
        //{
        //    titleText.text = quest[2].title;
        //    descriptionText.text = quest[2].description;
        //    goldText.text = quest[2].goldReward.ToString();
        //}
        //else
        //{

        //}
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

        //if (!quest[0].isCompleted && !quest[0].isCanceled)
        //{
        //    quest[0].isActive = true;
        //    player.quest = quest[0];
        //}
        //else if (!quest[1].isCompleted && !quest[1].isCanceled)
        //{
        //    quest[1].isActive = true;
        //    player.quest = quest[1];
        //}
        //else if (!quest[2].isCompleted && !quest[2].isCanceled)
        //{
        //    quest[2].isActive = true;
        //    player.quest = quest[2];
        //}
        //else
        //{

        //}

    }
}
