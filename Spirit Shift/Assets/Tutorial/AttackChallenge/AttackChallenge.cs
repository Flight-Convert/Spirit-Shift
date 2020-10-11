/*
 * Anthony Wessel
 * Project 1 (Spirit Shift)
 * 
 * A tutorial challenge which requires the player to
 * attack an enemy
 */

using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Attack")]
public class AttackChallenge : TutorialChallenge
{
    bool attacked;

    public GameObject[] enemiesToSpawn;

    // Initialize the boolean
    public override void Init(GameObject UIHolder)
    {
        attacked = false;

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
        if (attacked)
        {
            return true;
        }
        return false;
    }

    public override void UpdateCompletedTasks()
    {
        // If player clicked (attack button)
        if (Input.GetMouseButtonDown(0))
        {
            // Find the player's current body
            BasicMovement body = FindObjectOfType<BasicMovement>();

            // If the player is in an enemies body, then they attacked
            if (body.GetComponent<attackPlayer>() != null)
                attacked = true;
        }
    }
}
