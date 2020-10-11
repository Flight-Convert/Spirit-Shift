/*
 * Anthony Wessel
 * Project 1 (Spirit Shift)
 * 
 * A tutorial challenge which just shows some
 * extra info to the player and waits until
 * they press a button.
 */
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Tutorial/ExtraInfo")]
public class ExtraInfoChallenge : TutorialChallenge
{
    // Set up the array of booleans
    public override void Init(GameObject UIHolder)
    {
        tutorialUI = Instantiate(UIPanel, UIHolder.transform).GetComponent<TutorialUI>();
    }

    // returns true if this part of the tutorial is completed
    public override bool IsCompleted()
    {
        //Destroy(tutorialUI.gameObject);
        return false;
    }

    public override void UpdateCompletedTasks()
    {
    }
}
