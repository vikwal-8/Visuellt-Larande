using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTwo : MonoBehaviour
{
    private PlayerControllerWordsInSpace playerController;
    private readonly float timeActive = 8.0f, rotationSpeed = 90;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerWordsInSpace>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.one, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter()
    {
        {
            Pickup();
        }
    }
    void Pickup()
    {
        Debug.Log ("PowerupThree taken");
        Destroy(gameObject);

        // Make the player invincible
        playerController.Invincibility(timeActive);
    }
}
