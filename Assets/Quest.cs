using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isPending = true;
    public bool isUnlocked = false;
    public bool isActive = false; //Inprogress
    public bool isCompleted = false;
    public bool isDone = false;
    public bool isCanceled = false;

    public string title;
    public string description;
    public int goldReward;

    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        isCompleted = true;
    }
}
