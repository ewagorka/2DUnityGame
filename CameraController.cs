using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform follow;
    public Vector3 offset;                  //all parameters are public, so they can be adjusted in Unity
    public float smoothValue;       
    public Vector3 minBoundry;      
    public Vector3 maxBoundry;


    void Update()
    {
        Follow();
    }

    /// <summary>
    /// Makes camera follow the player and sets level boundaries.
    /// </summary>
    void Follow()
    {
        Vector3 targetPosition = follow.position + offset;

        Vector3 boundPosition = new Vector3(                                // For all x,y,z values
            Mathf.Clamp(targetPosition.x,minBoundry.x,maxBoundry.x),        // if players value on any axis is smaller than minBoundry, set boundPsoition value as minBoundry  
            Mathf.Clamp(targetPosition.y, minBoundry.y, maxBoundry.y),     // if players value on any axis is greater than maxBoundry, set boundPsoition value as maxBoundry 
            Mathf.Clamp(targetPosition.z, minBoundry.z, maxBoundry.z));     // if players value is in between of min and max, set boundPsoition value as players value
       
        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothValue*Time.fixedDeltaTime ); // Smooth camera movement by using Lerp function
        transform.position = smoothPosition;
    }
}

