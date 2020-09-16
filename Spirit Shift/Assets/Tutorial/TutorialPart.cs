using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPart : ScriptableObject
{
    public string prompt;

    public virtual bool IsCompleted()
    {
        return false;
    }

    public virtual void UpdateCompletedTasks()
    {

    }
}
