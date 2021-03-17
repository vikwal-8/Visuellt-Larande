using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IShopCustomer
{
    private float speed = 10f, rotationSpeed = 10f;
    private Animator anim;
    private Rigidbody rb;
    Camera cam;
  
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");    // Right/Left
        float verticalInput = Input.GetAxisRaw("Vertical");        // Up/Down

        // Check if player is trying to move
        if (horizontalInput != 0 || verticalInput != 0)
        {
            // Get direction of movement
            Vector3 movement = Camera.main.transform.right * horizontalInput + Camera.main.transform.forward * verticalInput;
            movement.y = 0;
            movement.Normalize();

            // Move player relative to camera direction
            transform.Translate(movement * Time.deltaTime * speed, Space.World);

            // Rotate player to direction of movement
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime);

            // Play move animation
            anim.SetBool("isMoving", true);
        }
        else {
            // play idle animation
            anim.SetBool("isMoving", false); 
        }
    }

    public void BoughtItem(Item.ItemType itemtype)
    {
        Debug.Log("bought item" + itemtype);
    }
}
