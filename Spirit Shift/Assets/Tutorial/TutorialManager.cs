/*
 * Anthony Wessel
 * Project 1 (Spirit Shift)
 * 
 * Main class for controlling tutorial. Has a list of all of the challenges
 */

using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool useTutorial;

    public TutorialChallenge[] challenges;

    public GameObject UIHolder;

    void Start()
    {
        // Start the first tutorial part (if it exists)
        if (useTutorial && challenges.Length > 0)
        {
            StartCoroutine(waitForChallengeCompletion(0));
        }

        // Set to not use the tutorial again when we restart the level
        useTutorial = false;
    }

    // Checks every frame to see if the tutorial part has been completed yet
    IEnumerator waitForChallengeCompletion(int challengeIndex)
    {
        // Get the current part
        TutorialChallenge challenge = challenges[challengeIndex];

        // Set up the current part
        challenge.Init(UIHolder);

        // Update the part until it is completed
        while(!challenge.IsCompleted())
        {
            challenge.UpdateCompletedTasks();
            yield return null;
        }

        // Move on to the next part, or end tutorial
        challengeIndex++;
        if (challengeIndex != challenges.Length)
            StartCoroutine(waitForChallengeCompletion(challengeIndex));
        else
            CompleteTutorial();
    }

    void CompleteTutorial()
    {
        Destroy(UIHolder.transform.parent.gameObject);
        print("Tutorial Completed!");
    }
}
