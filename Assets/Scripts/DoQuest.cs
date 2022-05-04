using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoQuest : MonoBehaviour
{
    public GameObject promptWindow;
    public Player player;

    private bool keyPressed = false;

    void OnTriggerEnter(Collider other)
    {
        //check if  (1) collides with player (2) has same goal type (3) is the target gameobject
        if (other.tag == "Player" && player.quest.goal.goalType == gameObject.GetComponentInParent<InteractableObject>().goalType && player.quest.target == gameObject.transform.parent.name)
        {
            promptWindow.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" & !keyPressed)
        {
            if (Input.GetKeyDown(KeyCode.E) && player.quest.goal.goalType == gameObject.GetComponentInParent<InteractableObject>().goalType && player.quest.target == gameObject.transform.parent.name)
            {
                player.CollectItem();
                keyPressed = true;
                if(player.quest.goal.goalType == GoalType.Collect)
                    Destroy(transform.parent.gameObject);
                promptWindow.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            promptWindow.SetActive(false);
        }
    }
}
