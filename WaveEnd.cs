
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveEnd : MonoBehaviour
{
    public GameObject[] enemy;
    private int enemyCount =0;
    private int waveCount =0;
    private int spawnAmount = 10;
    //wave start
   void start()
    {
        waveCount++;
        //  WILL NEED TO BE SET 
        Vector3 spawnPos = new Vector3();
        //create enemies and increase counter
        for (int x = 0; x < 10; x++)
        {
            Instantiate(enemy, spawnPos, enemy.transform.rotation);
            enemyCount++;
        }
        
    }
    void update()
    {
        if (enemyCount == 0)
        {
            spawnAmount = 10 + (waveCount * 2);
            //start next wave
            waveCount++;
            for (int x = 0; x < spawnAmount; x++)
            {
                Instantiate(enemy, spawnPos, enemy.transform.rotation);
                enemyCount++;
            }
        }
    }

    //detect enemy death and lower counter


}
