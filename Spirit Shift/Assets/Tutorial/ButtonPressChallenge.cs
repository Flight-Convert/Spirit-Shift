/*
 * Anthony Wessel
 * Project 1 (Spirit Shift)
 * 
 * A tutorial challenge which requires the player to
 * press a specific set of buttons (such as WASD)
 */
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/ButtonPress")]
public class ButtonPressChallenge : TutorialChallenge
{
    public KeyCode[] buttons;
    bool[] buttonsPressed;

    // Set up the array of booleans
    public override void Init()
    {
        buttonsPressed = new bool[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonsPressed[i] = false;
        }
    }

    // returns true if this part of the tutorial is completed
    public override bool IsCompleted()
    {
        // If any of the buttons has not been pressed, return false
        for (int i = 0; i < buttonsPressed.Length; i++)
        {
            if (!buttonsPressed[i]) return false;
        }

        return true;
    }

    public override void UpdateCompletedTasks()
    {
        // Check each button to see if it has been pressed
        for (int i = 0; i < buttons.Length; i++)
        {
            if (Input.GetKeyDown(buttons[i])) buttonsPressed[i] = true;
        }
    }
}
