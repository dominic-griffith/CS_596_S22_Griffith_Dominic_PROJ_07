using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPrompt : MonoBehaviour
{
    public QuestGiver questGiver;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            questGiver.OpenQuestWindow();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            questGiver.CloseQuestWindow();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
