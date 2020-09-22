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

    TutorialUI tutorialUI;

    // Initialize the boolean
    public override void Init(GameObject UIHolder)
    {
        attacked = false;

        tutorialUI = Instantiate(UIPanel, UIHolder.transform).GetComponent<TutorialUI>();
    }

    // returns true if this part of the tutorial is completed
    public override bool IsCompleted()
    {
        if (attacked)
        {
            Destroy(UIPanel.gameObject);
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
