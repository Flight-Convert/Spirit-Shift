/* Broc Edson
 * Riley Dalley
 * Spirit Shift
 * Spawns enemies
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public float xSpawnDistance;
    public float ySpawnDistance;
    public GameObject[] enemies;
    public Text waveText;
    public int initialEnemies = 8;
    public int waveEnemyMultiplier = 2;
    public int numEnemies;
    private int spawningEnemies = 10;
    public float spawnDelay = 1f;
    public float initialWaveTime = 3f;
    public static bool waveStart;
    public float additionalWaveTime = 5f;
    private int waveCount = 1;
    [HideInInspector]public float targetTime;
    private bool timing = false;

    private void Start()
    {
        waveStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(waveStart)
        {
            waveStart = false;
            StartCoroutine(SpawnEnemies());
        }
        if ((Time.time >= targetTime) && timing)
        {
            timing = false;
            spawningEnemies = initialEnemies + (waveCount * waveEnemyMultiplier);
            numEnemies = spawningEnemies;
            waveCount++;
            waveStart = true;
        }
        if(timing)
        {
            waveText.text = "Wave " + waveCount + ": " + (int)(targetTime - Time.time) + " s";
        }
    }

    IEnumerator SpawnEnemies()
    {
        if(waveCount == 1)
        {
            waveText.text = "Begin!";
        }
        else
        {
            waveText.text = "Wave Complete!";
        }
        yield return new WaitForSeconds(initialWaveTime);
        timing = true;
        targetTime = Time.time + ((initialEnemies + (waveCount * waveEnemyMultiplier)) * spawnDelay) + additionalWaveTime;
        for (int i = 0; i < spawningEnemies; i++)
        {
            while (FindObjectOfType<PauseMenu>().paused)
            {
                yield return null;
            }
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
            yield return new WaitForSeconds(spawnDelay);
        }
        yield return true;
    }

    public void EnemyDestroyed()
    {
        numEnemies--;
    }
}
