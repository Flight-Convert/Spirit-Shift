using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// By: Wolfgang Gross
// For: CIS 491 - Team 2 Project


public class SwitchBehavior : MonoBehaviour
{
    public int currentBehavior = 0; //0 = unassigned, 1 = enemy, 
                                    //2 = player, 3 = player-inactive

    public int previousBehavior = 0; //Check what the unit was before

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Player")
        {
            setBehavior(2);
        }
        else if(gameObject.tag == "Enemy")
        {
            setBehavior(1);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getBehavior()
    {
        return currentBehavior;
    }
    public void setBehavior(int x)
    {
        if(previousBehavior != currentBehavior)
        {
            previousBehavior = currentBehavior;
        }
        currentBehavior = x;
    }
}
