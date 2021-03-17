using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpOne : MonoBehaviour
{
    private PlayerControllerWordsInSpace playerController;
    private readonly float amount = 10.0f, rotationSpeed = 90;

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
        Pickup();
    }

    void Pickup()
    {
        Debug.Log("PowerupOne taken");
        Destroy(gameObject);

        // Fuel-up the player ship
        playerController.IncreaseFuel(amount);
    }
}
