using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CameraStyle { Fixed, Free }
public class CameraController : MonoBehaviour
{

    public GameObject player;
    public CameraStyle cameraStyle;
    public Transform pivot;
    public float rotationSpeed = 1f;

    private Vector3 offset;
    private Vector3 pivotOffset;

    // Start is called before the first frame update
    void Start()
    {
        //Offset of the pivot relative to player
        pivotOffset = pivot.position - player.transform.position;
        //Offset of the player
        offset = transform.position - player.transform.position;
        //Set the offset of the camera based on cameRA
        offset = transform.position - player.transform.position;  
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (cameraStyle == CameraStyle.Fixed)
        {
            //Get the cameras transform position to be that of the players transform position
            transform.position = player.transform.position + offset;
        }

        if (cameraStyle == CameraStyle.Free)
        {
            //pivot follow player
            pivot.transform.position = player.transform.position + pivotOffset;
            Quaternion turnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            offset = turnAngle * offset;
            //Settign camera position to that of pivot plus the offset
            transform.position = pivot.transform.position + offset;
            //make camera look at pivot
            transform.LookAt(pivot);
        }
      
    }
}
