using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    public GameObject questLogWindow;

    public QuestGiver questGiver;

    public TextMeshProUGUI questInfo;

    //public Button killCancel;
    //public Button gatherCancel;
    //public Button activateCancel;

    private bool questLogOpen = false;

    private void updateQuestLog()
    {
        questInfo.text = "";
        for (int i = 0; i < questGiver.quest.Count; i++)
        {
            if(questGiver.quest[i].isCompleted)
            {
                questInfo.text += questGiver.quest[i].title + "..........Completed\n";
            }
            else if (questGiver.quest[i].isCanceled)
            {
                questInfo.text += questGiver.quest[i].title + "..........Canceled\n";
            }
            else if(questGiver.quest[i].isDone)
            {
                questInfo.text += questGiver.quest[i].title + "..........Done\n";
            }
            else if(questGiver.quest[i].isActive)
            {
                questInfo.text += questGiver.quest[i].title + "..........Active" + "        (" + questGiver.quest[i].goal.currentAmount + "/" + questGiver.quest[i].goal.requiredAmount + ")\n";
            }
            else if(questGiver.quest[i].isUnlocked)
            {
                questInfo.text += questGiver.quest[i].title + "..........Unlocked\n";
            }
            else
            {
                questInfo.text += questGiver.quest[i].title + "..........Pending\n";
            }
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q) && !questLogOpen)
        {
            updateQuestLog();
            questLogWindow.SetActive(true);
            questLogOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && questLogOpen)
        {
            questLogWindow.SetActive(false);
            questLogOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
