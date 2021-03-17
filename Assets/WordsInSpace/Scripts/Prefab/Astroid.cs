using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    Transform trans;
    Vector3 rotation;

    private float min = 4f;
    private float max = 8f;
   

    
    void Awake(){
        trans = transform;
    }

    void Start(){

        // randomizing astriod size
        Vector3 scale = Vector3.one; 
        scale.x = Random.Range(min, max);
        scale.y = Random.Range(min, max);
        scale.z = Random.Range(min, max);

        trans.localScale = scale;

        // random rotation
        // rotation.x = Random.Range(-rotationOffset, rotationOffset);
        // rotation.y = Random.Range(-rotationOffset, rotationOffset);
        // rotation.z = Random.Range(-rotationOffset, rotationOffset);
    }

    // void Update(){
    //     trans.Rotate(rotation * Time.deltaTime);
    // }
}
