using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    Transform target;
    public Laser laser;
  
    Vector3 hitPosition;


   void Start(){
       target = GameObject.FindGameObjectWithTag("Player").transform;
       
   }

    void Update(){
        InFront();
        HaveLineOfSight();
        
        //Debug.Log("UTANFÖR");
        if(InFront() && HaveLineOfSight())
        {
          //  Debug.Log("ENEMY SHOOT");
            FireLaser();
        }
    }
    
    //Checking if target is inside the attack angle
    bool InFront(){

        // Calculating angle to target (Player)
        Vector3 directionToTarget = transform.position - target.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        if(Mathf.Abs(angle) > 160 && Mathf.Abs(angle) < 200)
        {
            Debug.DrawLine(transform.position, target.position, Color.green);
            return true;
        }

        Debug.DrawLine(transform.position, target.position, Color.yellow);
        return false;
    }


    bool HaveLineOfSight(){
        RaycastHit hit;

        Vector3 direction = target.position - transform.position;
        //Debug.DrawRay(laser.transform.position, direction, Color.red);

        if(Physics.Raycast(laser.transform.position, direction, out hit, laser.Distance))
        {
            //Debug.Log("FÖRSTA IF!!!");
            if(hit.transform.CompareTag("Player"))
            {
               // Debug.Log("Andra IF!!!");
                Debug.DrawRay(laser.transform.position, direction, Color.green);
                hitPosition = hit.transform.position;
                return true;
            }
        }
        return false;
    }

    void FireLaser(){
        laser.FireLaser(hitPosition, target);
    }

    
}
