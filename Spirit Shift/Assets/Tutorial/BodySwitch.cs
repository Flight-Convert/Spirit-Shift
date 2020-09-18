﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/BodySwitch")]
public class BodySwitch : TutorialPart
{
    public enum Target
    {
        Player,
        Enemy,
        RedTeam,
        BlueTeam,
        Anyone
    }

    public Target target;
    bool switchedToTarget;
    GameObject currentBody;

    // Set up the array of booleans
    public override void Init()
    {
        // Find the player's current body
        currentBody = FindObjectOfType<BasicMovement>().gameObject;
        switchedToTarget = false;
    }

    // returns true if this part of the tutorial is completed
    public override bool IsCompleted()
    {
        return switchedToTarget;
    }
    public override void UpdateCompletedTasks()
    {
        // Find the player body this frame
        GameObject body = FindObjectOfType<BasicMovement>().gameObject;

        // If the player switched bodies
        if (body != currentBody)
        {
            // If the goal was to just switch bodies, we are done
            if (target == Target.Anyone)
            {
                switchedToTarget = true;
                return;
            }
               
            // If the goal is to switch to the player and we did
            if (target == Target.Player && body.CompareTag("Player"))
            {
                switchedToTarget = true;
                return;
            }
            
            // If we switched to an enemy
            else if (body.CompareTag("Enemy"))
            {
                // Get the current layer (used to differentiate team)
                int layer = body.layer;

                // If the goal is just to switch to an enemy, we are done
                if (target == Target.Enemy)
                    switchedToTarget = true;

                // If the goal is to switch to red team and we did
                else if (target == Target.RedTeam && layer == LayerMask.NameToLayer("RedTeam"))
                    switchedToTarget = true;

                // If the goal is to switch to blue team and we did
                else if (target == Target.BlueTeam && layer == LayerMask.NameToLayer("BlueTeam"))
                    switchedToTarget = true;
            }

            // Update the current body
            currentBody = body;
        }
        
    }
}