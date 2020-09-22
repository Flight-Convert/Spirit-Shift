/*
 * Anthony Wessel
 * Project 1 (Spirit Shift)
 * 
 * Base class for tutorial challenges
 */

using UnityEngine;

public abstract class TutorialChallenge : ScriptableObject
{
    public GameObject UIPanel;

    public abstract void Init(GameObject UIHolder);

    public abstract bool IsCompleted();

    public abstract void UpdateCompletedTasks();
}
