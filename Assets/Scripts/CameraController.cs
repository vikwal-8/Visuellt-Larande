using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // player
    public bool paused; 

    // camera offsets from player
    private float xCamOffset = 5.0f;
    private float yCamOffset = 2.0f;

    // camera rotaion speeds
    private float xSpeed = 50.0f;
    private float ySpeed = 20.0f;

    // min/max rotation angles
    private float yMinLimit = -20f;
    private float yMaxLimit = 60f;

    // zoom distances
    private float distanceMin = 5f;
    private float distanceMax = 10f;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        Cursor.lockState = CursorLockMode.Locked; // Lock cursor at center of screen
        Cursor.visible = false; // hide cursor
    }

    void LateUpdate()
    {
        if (target && !paused)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            xCamOffset = Mathf.Clamp(xCamOffset - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                xCamOffset -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, yCamOffset, -xCamOffset);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
