using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    //Change radius in subclass to change range for interaction
    public float radius = 3f;

    public Transform player;
    bool hasInteracted = false;
    

    

    public virtual void Interacting()
    {
      // Override this interact
    }

    public virtual void CloseInteract()
    {
        // Override this CloseInteract
    }



    
    void Update()
    {
        
        float distance = Vector3.Distance(player.position, transform.position);

        // Check if player is in radius for the interactble object to interact. Also check if it has already interacted.
        if (!hasInteracted)
        {
            
            if (distance <= radius)
            {
                Interacting();
                hasInteracted = true;
                Debug.Log("Interacting with " );
            }
        }

        //Set hasInteracted to false when player moved away, this grants the player able to interact agian.
        if(hasInteracted && (distance > radius + 3))
        {
            Debug.Log("Stopped interacting");
            hasInteracted = false;
        }
    }

    // drawing gizmo to see radius.
    void OnDrawGizmosSelected()
    {
      
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


  

}