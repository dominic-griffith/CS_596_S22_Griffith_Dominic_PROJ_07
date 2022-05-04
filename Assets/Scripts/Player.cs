using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int gold;

    //make list for more quest
    public Quest quest;

    public TextMeshProUGUI goldCounter;

    private void Start()
    {
        gold = 0;
    }

    private void Update()
    {
        goldCounter.text = gold.ToString();
    }

    public void CollectItem()
    {
        if(quest.isActive)
        {
            //add if to check here to see if player is working towards active quest
            quest.goal.Collected();
            if(quest.goal.isReached())
            {
                gold += quest.goldReward;
                quest.Complete();
            }
        }
    }
}
