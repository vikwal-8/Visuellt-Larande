using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 normalOffset = new Vector3(0, 6, -15);
    private Vector3 boostOffset = new Vector3(0, 6, -16);

    private readonly float lerpDuration = 0.25f;
    
    public float transitionDuration;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transitionDuration = transitionDuration >= lerpDuration ? lerpDuration : transitionDuration += Time.deltaTime;
        }
        else
        {
            transitionDuration = transitionDuration <= 0 ? 0 : transitionDuration -= Time.deltaTime;
        }
        transform.localPosition = Vector3.Lerp(normalOffset, boostOffset, transitionDuration/lerpDuration);
    }
}
