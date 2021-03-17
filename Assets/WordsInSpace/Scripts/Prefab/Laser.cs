using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
   LineRenderer lineRenderer;
   PlayerControllerWordsInSpace playerController;
   bool canFire ;
   private float laserOnTime = 0.07f;
   private float laserDistance = 50f;
   private float fireDelay = 0.2f;
   private readonly float consumption = 20.0f;
    GameManager gameManager;

   void Awake() {
       lineRenderer = GetComponent<LineRenderer>();
       
   }

   void Start(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerWordsInSpace>();
       lineRenderer.enabled = false;
       canFire = true;
   }
   
  

   Vector3 CastRay(){

       //Raycasthit -structure to get info back from raycast
       RaycastHit hit;

       //calulating the maxDistance parameter for raycast
       Vector3 fwd = transform.TransformDirection(Vector3.forward) * laserDistance;
 

       // shooting raycast, if hit we get info and explosion occurs
       // also returns vector3 of pos
       if(Physics.Raycast(transform.position, fwd, out hit))
       {
           Debug.Log("WE hit: " + hit.transform.name);

           StartExplosion(hit.point, hit.transform);
           return hit.point;
       }
        
        //return a vector3 
        Debug.Log("MISSED!");
        return transform.position + (transform.forward * laserDistance);
       
   }

   void StartExplosion(Vector3 hitPos, Transform target){
        if (target.gameObject.name == "Collider")
        {
            Explosion temp = target.parent.gameObject.GetComponent<Explosion>();
            if (temp != null)
            {
                temp.BeenHit(target);
            }
        }
   }

    
   public void FireLaser()
   {
      Vector3 pos = CastRay();
      FireLaser(pos);  
   }

    //Overloading FireLaser
   public void FireLaser(Vector3 targetPositon, Transform target = null)
   {
       //check if we can fire laser
        if(canFire){
            if(target != null)
            {
                if (gameManager.GetLastBoss())
                {
                    playerController.ConstantFuelConsumption(consumption*100);
                }
                else
                {
                  
                    playerController.ConstantFuelConsumption(consumption);
         
                }
            }
            
            //Setting start point and target point for laser
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, targetPositon);
            lineRenderer.enabled = true;

            // setting bool to false to cooldown laser
            canFire = false;

            Invoke("TurnOffLaser", laserOnTime);
            Invoke("CanFire", fireDelay);
       }  
   }




   void TurnOffLaser(){
       lineRenderer.enabled = false;
       canFire = true;
   }

    // Method that reutnrs the distance
   public float Distance{
       get { return laserDistance;}
   }
    // reset canFire to true
   void CanFire(){
       canFire = true;
   }
}
