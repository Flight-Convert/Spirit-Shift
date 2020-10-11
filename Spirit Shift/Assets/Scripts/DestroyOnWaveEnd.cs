using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnWaveEnd : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private bool gameOver;
    private float targetTime;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<PlayerHealth>();
        if (FindObjectOfType<TutorialManager>()) Destroy(this);
        targetTime = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>().targetTime;
    }

    // Update is called once per frame
    void Update()
    {
        gameOver = playerHealth.GetGameOver();
        if (gameOver)
        {
            //Don't do behavior
        }
        else
        {
            //play the game
            if (Time.time >= targetTime)
            {
                Destroy(gameObject);
            }
        }
    }

}
