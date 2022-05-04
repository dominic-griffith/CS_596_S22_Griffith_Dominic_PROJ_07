using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{

    [System.NonSerialized] public bool isPending = true;
    [System.NonSerialized] public bool isUnlocked = false;
    [System.NonSerialized] public bool isActive = false; //Inprogress
    [System.NonSerialized] public bool isCompleted = false;
    [System.NonSerialized] public bool isDone = false;
    [System.NonSerialized] public bool isCanceled = false;

    public string title;
    public string description;
    public int goldReward;

    public string target;

    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        isCompleted = true;
    }
}
