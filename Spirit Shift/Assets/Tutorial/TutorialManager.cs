using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool useTutorial;

    public TutorialPart[] tutorialParts;

    void Start()
    {
        // Start the first tutorial part (if it exists)
        if (useTutorial && tutorialParts.Length > 0)
        {
            StartCoroutine(waitForTutorialPartCompletion(0));
        }

        // Set to not use the tutorial again when we restart the level
        useTutorial = false;
    }

    // Checks every frame to see if the tutorial part has been completed yet
    IEnumerator waitForTutorialPartCompletion(int tutorialIndex)
    {
        // Get the current part
        TutorialPart part = tutorialParts[tutorialIndex];

        // Set up the current part
        part.Init();
        print(part.prompt);

        // Update the part until it is completed
        while(!part.IsCompleted())
        {
            part.UpdateCompletedTasks();
            yield return null;
        }

        // Move on to the next part, or end tutorial
        tutorialIndex++;
        if (tutorialIndex != tutorialParts.Length)
            StartCoroutine(waitForTutorialPartCompletion(tutorialIndex));
        else
            print("Tutorial Completed!");
    }
}
