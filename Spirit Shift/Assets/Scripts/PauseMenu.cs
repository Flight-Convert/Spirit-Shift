/*
 * Anthony Wessel
 * Project 1 (Spirit Shift)
 * 
 * Controls the pause menu and whether or not
 * the game is paused
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Keeps track of whether or not the game is paused
    public bool paused;
    public GameObject tutorialTips;

    // Start is called before the first frame update
    void Start()
    {
        UnpauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        // If the game is currently paused
        if (paused)
        {
            // Unpause the game when the player presses escape
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnpauseGame();
            }

        }
        // If the game is not paused
        else
        {
            // Pause the game when the player presses escape
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }   
        }
    }

    // Pause the game
    public void PauseGame()
    {
        paused = true;
        transform.GetChild(0).gameObject.SetActive(true);
        if (SceneManager.GetActiveScene().name == "TutorialScene")
            tutorialTips.SetActive(true);
        else
            tutorialTips.SetActive(false);
    }

    // Unpause the game
    public void UnpauseGame()
    {
        paused = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Restart the current game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Load the tutorial level
    public void LoadTutorialLevel()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
