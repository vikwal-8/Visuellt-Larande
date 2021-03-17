using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowers : MonoBehaviour
{
    List<GameObject> prefabList = new List<GameObject>();

    public GameObject PowerGetFuelPrefab, PowerInvincibilityPrefab;


    public Vector3 center;
    public Vector3 size;
    private float worldSize;
    public float spawnTime;
    private float time;
    public float spawnMostWait = 9;
    public float spawnLeastWait = 4;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        // adds powers to list so we can randomize
        prefabList.Add(PowerInvincibilityPrefab);
        prefabList.Add(PowerGetFuelPrefab);
        worldSize = gameManager.GetWorldSize();
        // makes cube within which powers spawn
        size.x = worldSize;
        size.y = worldSize;
        size.z = worldSize;

        RandomTime();
        time = spawnLeastWait;

        SpawnPower();
    }

    // Update is called once per frame
    void Update()
    {
        // Counts up
        time += Time.deltaTime;

        // Check if its the right time to spawn the object
        if (time >= spawnTime && !gameManager.GetLastBoss())
        {
            SpawnPower();
            RandomTime();
        }
    }

    public void SpawnPower()
    {
        time = 0;
        Vector3 pos = UnityEngine.Random.insideUnitSphere * worldSize;
        if (Random.Range(0, 100) < 70)
        {
            Instantiate(prefabList[1], pos, Quaternion.identity);
        }
        else
        {
            Instantiate(prefabList[0], pos, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition + center, size);
    }

    void RandomTime()
    {
        spawnTime = (Random.Range(spawnLeastWait, spawnMostWait)*gameManager.GetLevel());
    }
}
