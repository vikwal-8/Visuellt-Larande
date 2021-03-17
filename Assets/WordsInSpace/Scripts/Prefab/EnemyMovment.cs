using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
   Transform target;
   private float rotationSpeed = 0.7f;
   private float movmentSpeed = 10f;
   private float rayOffset = 2f;
   private float detectDistance = 20f;
   private int recievedHits;


   void Start(){
       target = GameObject.FindGameObjectWithTag("Player").transform;
   }

   void Update(){

       

       Pathdetection();
      // Turn();
       Forward();

   }
    // makes enemy go forward..
   void Forward(){
      transform.position += transform.forward * Time.deltaTime * movmentSpeed; 
   }

   void Turn(){
    //make enemy turn slow towards target(player) with Quaternion

    Vector3 position = target.position - transform.position;
    //float distance = Vector3.Distance(target.position, transform.position);
    //Debug.Log(distance);
    Quaternion rotation = Quaternion.LookRotation(position);

    // sets enemy rotation to target  with a slerp function to turn with speed
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

   }

   void Pathdetection(){

       // casyrays for detecting objects to avoid collision
      
      
       RaycastHit detection;
       Vector3 raycastOffset = Vector3.zero;

       
       Vector3 left = transform.position - transform.right * rayOffset;
       Vector3 right = transform.position + transform.right * rayOffset;
       Vector3 up = transform.position + transform.up * rayOffset;
       Vector3 down = transform.position - transform.up * rayOffset;

       //drawing rays for debugging
       Debug.DrawRay(left, transform.forward * detectDistance, Color.cyan);
       Debug.DrawRay(right, transform.forward * detectDistance, Color.cyan);
       Debug.DrawRay(up, transform.forward * detectDistance, Color.cyan);
       Debug.DrawRay(down, transform.forward * detectDistance, Color.cyan);


        //Checking if any of the rays detect, change direction if its true.
        // If not then Turn normal towards player.

        if(Physics.Raycast(left, transform.forward, out detection, detectDistance))
       {
           raycastOffset += Vector3.right;
       }
        else if(Physics.Raycast(right, transform.forward, out detection, detectDistance))
       {
           raycastOffset -= Vector3.right;
       }



        if(Physics.Raycast(up, transform.forward, out detection, detectDistance))
       {
           raycastOffset -= Vector3.up;
       }
        else if(Physics.Raycast(down, transform.forward, out detection, detectDistance))
       {
           raycastOffset += Vector3.up;
       }


       // Check if raycastOffset has been modified and change course if it has
       if(raycastOffset != Vector3.zero)
       {
           transform.Rotate(raycastOffset * 5f * Time.deltaTime);
       }else
       {
           Turn();
       }

   }

   public int GetRecievedHits()
    {
        return recievedHits;
    }
    public void UpdateRecievedHits()
    {
        recievedHits++;
    }


}
