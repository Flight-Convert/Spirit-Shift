using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextChallengeButton : MonoBehaviour
{
    public void NextChallenge()
    {
        TutorialManager.nextPressed = true;
    }
}
