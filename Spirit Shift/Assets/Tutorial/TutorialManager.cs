using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool useTutorial;

    public TutorialPart[] tutorialParts;

    // Start is called before the first frame update
    void Start()
    {
        if (useTutorial && tutorialParts.Length > 0)
        {
            StartCoroutine(waitForTutorialPartCompletion(0));
        }
        useTutorial = false;
    }

    IEnumerator waitForTutorialPartCompletion(int tutorialIndex)
    {
        TutorialPart part = tutorialParts[tutorialIndex];

        print(part.prompt);

        while(!part.IsCompleted())
        {
            part.UpdateCompletedTasks();
            yield return null;
        }

        tutorialIndex++;
        if (tutorialIndex != tutorialParts.Length)
            StartCoroutine(waitForTutorialPartCompletion(tutorialIndex));
        else
            print("Tutorial Completed!");
    }
}
