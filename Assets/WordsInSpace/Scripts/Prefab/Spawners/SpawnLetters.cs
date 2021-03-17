using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnLetters : MonoBehaviour
{
    public TextMeshPro letter;
    private float worldSize;
    private int spawnedLettersCount = 0;
    GameManager gamemanager;
    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        worldSize = gamemanager.GetWorldSize();
        // Spawn 3 letters at start.
        for (int i = 0; i < 2; i++) 
        {
            spawnLetter();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //If more letters then 38 are added they will not fit on the canvas for wordplay.
    public void spawnLetter()
    {
        if (spawnedLettersCount <= 30)
        {
            TextMeshPro temp = Instantiate(letter, UnityEngine.Random.insideUnitSphere * worldSize, Quaternion.identity);
            temp.gameObject.GetComponent<Letter>().GetSpawner(this); //Send "this" so when OnTrigger for each letter is called, we can reference to SpawnLetter again.
            spawnedLettersCount++;
            if(spawnedLettersCount%5 == 0)
            {
                gamemanager.IncreaseLevel();
            }
        }
        if(spawnedLettersCount >= 30)
        {
            gamemanager.SetLastBoss();
        }
    }
}

