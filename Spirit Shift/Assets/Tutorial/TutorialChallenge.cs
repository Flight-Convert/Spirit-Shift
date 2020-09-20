/*
 * Anthony Wessel
 * Project 1 (Spririt Shift)
 * 
 * Base class for tutorial challenges
 */

using UnityEngine;

public abstract class TutorialChallenge : ScriptableObject
{
    public string prompt;

    public abstract void Init();

    public abstract bool IsCompleted();

    public abstract void UpdateCompletedTasks();
}
