/* Broc Edson
 * Spirit Shift
 * Spawns enemies
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float xSpawnDistance;
    public float ySpawnDistance;
    public GameObject[] enemies;
    public int numEnemies;
    public static bool waveStart = true;

    // Update is called once per frame
    void Update()
    {
        if(waveStart)
        {
            waveStart = false;
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        while(numEnemies > 0)
        {
            int side = Random.Range(0, 4) + 1;
            int enemyIndex = Random.Range(0, enemies.Length);
            float placeGradient = Random.Range(0f, 1f);
            switch (side)
            {
                case 1:
                    Instantiate(enemies[enemyIndex], new Vector3((placeGradient * (xSpawnDistance * 2)) - xSpawnDistance, ySpawnDistance, 0), enemies[enemyIndex].transform.rotation);
                    break;
                case 2:
                    Instantiate(enemies[enemyIndex], new Vector3(xSpawnDistance, (placeGradient * (ySpawnDistance * 2)) - ySpawnDistance, 0), enemies[enemyIndex].transform.rotation);
                    break;
                case 3:
                    Instantiate(enemies[enemyIndex], new Vector3((placeGradient * (xSpawnDistance * 2)) - xSpawnDistance, -ySpawnDistance, 0), enemies[enemyIndex].transform.rotation);
                    break;
                default:
                    Instantiate(enemies[enemyIndex], new Vector3(-xSpawnDistance, (placeGradient * (ySpawnDistance * 2)) - ySpawnDistance, 0), enemies[enemyIndex].transform.rotation);
                    break;
            }
            numEnemies--;
        }
    }
}
