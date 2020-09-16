using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Movement")]
public class TutorialPartMovement : TutorialPart
{
    bool wPressed = false;
    bool aPressed = false;
    bool sPressed = false;
    bool dPressed = false;

    public override bool IsCompleted()
    {
        return wPressed && aPressed && sPressed && dPressed;
    }
    public override void UpdateCompletedTasks()
    {
        if (Input.GetKey(KeyCode.W)) wPressed = true;
        if (Input.GetKey(KeyCode.A)) aPressed = true;
        if (Input.GetKey(KeyCode.S)) sPressed = true;
        if (Input.GetKey(KeyCode.D)) dPressed = true;
    }
}
