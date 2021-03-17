using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnDelay = 20f;
    private float worldSize;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        worldSize = gameManager.GetWorldSize();
        StartCoroutine(SpawnEnemy());
    }


    // spawning enemy prefab 
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (gameManager.GetLastBoss())
            {
                GameObject temp = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere * worldSize, Quaternion.identity);
                temp.transform.localScale += new Vector3(1, 1, 1);
            }
            else if (!gameManager.GetLastBoss())
                Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere * worldSize, Quaternion.identity);

            while (!gameManager.isGameActive) {
                yield return new WaitForSecondsRealtime(0.1f);
            }

            yield return new WaitForSeconds(spawnDelay / gameManager.GetLevel());
        }
        
    }
}
