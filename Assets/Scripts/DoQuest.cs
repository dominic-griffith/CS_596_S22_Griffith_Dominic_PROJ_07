using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoQuest : MonoBehaviour
{
    private GameObject promptWindow;
    private Player player;

    public string interactableObject = "Null";

    private bool keyPressed = false;

    private void Start()
    {
        promptWindow = GameObject.Find("Canvas").transform.Find("Interaction Prompt").gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
        }
        //check if  (1) collides with player (2) has same goal type (3) is the target gameobject
        if (other.tag == "Player" &&  player.quest.target == interactableObject)
        //add this for gameobjects with different quest types: player.quest.goal.goalType == gameObject.GetComponentInParent<InteractableObject>().goalType &&
        {
            promptWindow.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
        }
        if (other.tag == "Player" & !keyPressed)
        {
            if (Input.GetKeyDown(KeyCode.E) && player.quest.target == interactableObject)
            //same here: player.quest.goal.goalType == gameObject.GetComponentInParent<InteractableObject>().goalType &&
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
