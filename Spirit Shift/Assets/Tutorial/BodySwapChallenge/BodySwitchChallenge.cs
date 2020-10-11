/*
 * Anthony Wessel
 * Project 1 (Spirit Shift)
 * 
 * A tutorial challenge which requires the player to
 * switch to a specific body/enemy
 */

using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/BodySwitch")]
public class BodySwitchChallenge : TutorialChallenge
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

    public GameObject[] enemiesToSpawn;

    // Set up the array of booleans
    public override void Init(GameObject UIHolder)
    {
        // Find the player's current body
        currentBody = FindObjectOfType<BasicMovement>().gameObject;
        switchedToTarget = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) player = GameObject.FindGameObjectWithTag("Player Inactive");
        Vector3 playerPos = player.transform.position;

        // Spawn enemies around the player
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(1, 3), Random.Range(1, 3), 0);
            spawnPos = spawnPos.normalized * 6;

            Instantiate(enemiesToSpawn[i], playerPos + spawnPos, Quaternion.identity);
        }

        tutorialUI = Instantiate(UIPanel, UIHolder.transform).GetComponent<TutorialUI>();
    }

    // returns true if this part of the tutorial is completed
    public override bool IsCompleted()
    {
        if (switchedToTarget)
        {
            return true;
        }

        return false;
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
