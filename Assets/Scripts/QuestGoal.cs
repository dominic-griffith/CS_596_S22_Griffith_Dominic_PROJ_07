using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool isReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void Found()
    {
        if (goalType == GoalType.Find)
            currentAmount++;
    }

    public void Collected()
    {
        if (goalType == GoalType.Collect)
            currentAmount++;
    }
}

public enum GoalType
{
    Find,
    Collect,
    None
}