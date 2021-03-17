using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OutOfBoundsChecker : MonoBehaviour
{
    private GameObject player, counterGUI, centerArrow;
    private GameManager gameManager;
    private float timeBeforeRespawn, currentTime;
    public bool outOfBounds;
    float worldDistance;
    Vector3 center = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = gameManager.FindInActiveObjectByTag("Player");
        counterGUI = gameManager.FindInActiveObjectByTag("RespawnCount");
        centerArrow = gameManager.FindInActiveObjectByTag("CenterArrow");
        timeBeforeRespawn = 10.0f;
        worldDistance = gameManager.GetWorldSize();
        centerArrow.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {


        if (player.transform.position.sqrMagnitude > worldDistance*worldDistance && !outOfBounds) //Player goes out of bounds.
        {
            outOfBounds = true;
            currentTime = Time.time;
            StartCoroutine(CountdownPrint());
            gameManager.OutOfBoundsScreen(true);
            updateRotationDirection();



        }
        else if(player.transform.position.sqrMagnitude > worldDistance*worldDistance) // player is still of bounds
        {
            updateRotationDirection();
            if (Time.time - currentTime > timeBeforeRespawn)
            {

                player.transform.position = UnityEngine.Random.insideUnitSphere * worldDistance;
            }
        }
        else if(player.transform.position.sqrMagnitude < worldDistance*worldDistance && outOfBounds) //Player has entered back to bounds after going out of bounds.
        {
            outOfBounds = false;
            gameManager.OutOfBoundsScreen(false);
        }

    }

    IEnumerator CountdownPrint()
    {
        while (outOfBounds)
        {
            counterGUI.GetComponent<TextMeshProUGUI>().text = "Du teleporteras tillbaka om " + (int)Math.Round((timeBeforeRespawn - (Time.time - currentTime)), 0);
            yield return new WaitForSeconds(1.0f);

        }

    }

    void updateRotationDirection()
    {
        centerArrow.transform.LookAt(center);
        centerArrow.transform.rotation *= Quaternion.Euler(0, 90, 90);
    }

    // Finds inactive gameobjects
}
