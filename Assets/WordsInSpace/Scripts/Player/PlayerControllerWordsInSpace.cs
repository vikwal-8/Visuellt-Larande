using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerWordsInSpace : MonoBehaviour
{
    private readonly float basicSpeed = 20.0f, boostSpeed = 100.0f, turnSpeed = 90.0f, rollSpeed = 100.0f;
    private readonly float maxFuel = 120.0f, basicConsumption = 1.0f, boostConsumption = 5.0f;
    private float currentFuel, consumption;
    private AudioSource audioSource;
    public AudioClip fuelPickUpAudio, starPickUpAudio;
    private bool invincible = false;
    public Image fuel;
    private GameManager gameManager;
    Transform trans;
    public Laser[] laser;

    void Awake()
    {
        trans = transform;
    }

    void Start()
    {
        //fuel = GetComponent<Image>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentFuel = maxFuel;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        if (Input.GetKey(KeyCode.Space))
        {
            speed = boostSpeed;
            consumption = boostConsumption;
        }
        else
        {
            speed = basicSpeed;
            consumption = basicConsumption;
        }

        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");

        // Translates the ship forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // Turns the ship
        // transform.Rotate(Vector3.up, Time.deltaTime * horizontalInput * turnSpeed);
        // transform.Rotate(Vector3.right, Time.deltaTime * verticalInput * turnSpeed);
        Turn();

        // Fire the laser
        FireLaser();

        // Updates the fuelbar
        ConstantFuelConsumption(consumption);

        // Track currentFuel
        TrackCurrentFuel();
    }

    private void TrackCurrentFuel()
    {
        if (currentFuel <= 0 && gameManager.isGameActive)
        {
            gameManager.GameOver();
        }
        else if (currentFuel >= maxFuel)
        {
            currentFuel = maxFuel;
        }
    }

    // Constantly updates the fuel based on the consumption multiplier and time
    public void ConstantFuelConsumption(float consumption)
    {
        if (!invincible)
        {
            currentFuel -= Time.deltaTime * consumption;
            fuel.fillAmount = currentFuel / maxFuel;
        }
    }

    // Adds "amount" of fuel to currentfuel
    public void IncreaseFuel(float amount)
    {
        audioSource.PlayOneShot(fuelPickUpAudio);
        currentFuel += amount;
        fuel.fillAmount = currentFuel / maxFuel;
    }

    void Turn()
    {
        // input * turnspeed * time.DeltaTime
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        float roll = rollSpeed * Time.deltaTime * Input.GetAxis("Roll");

        trans.Rotate(pitch, yaw, -roll);
    }

    // Set invincibility power-up to true and start countdown
    public void Invincibility(float sec)
    {
        invincible = true;
        audioSource.PlayOneShot(starPickUpAudio);
        fuel.GetComponent<Image>().color = Color.yellow;
        StartCoroutine(PowerUpCountdown(sec));
    }

    // Countdown for the invicibility power up
    IEnumerator PowerUpCountdown(float sec)
    {
        yield return new WaitForSeconds(sec);
        fuel.GetComponent<Image>().color = new Color32(38, 164, 131, 255);
        invincible = false;
    }

    // Check for collision and destroy the other object...
    // ...if the invincibility power-up is active
    private void OnCollisionEnter(Collision collision)
    {
        if (invincible)
        {
            Destroy(collision.gameObject);
        }
    }

    void FireLaser()
    {
        // Checking if player activates laser
        if (Input.GetKeyDown(KeyCode.Mouse0) && gameManager.isGameActive){
            
            // Loop through all laser and fires them
            foreach(Laser l in laser)
            {
                Vector3 pos = transform.position + (transform.forward * l.Distance);
                l.FireLaser();
            }
        }
    }
}
