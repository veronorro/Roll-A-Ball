using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject myPartner;
    public bool canTeleport = true;

    public float height;

    private void Start()
    {
        canTeleport = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && canTeleport)
        {
            myPartner.GetComponent<Teleporter>().canTeleport = false;
            //Offset the y position so we don't clip into ground
            Vector3 endPos = new Vector3(myPartner.transform.position.x, myPartner.transform.position.y + height, myPartner.transform.position.z);
            other.transform.position = endPos;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !canTeleport)
            canTeleport = true;
    }
}
  
    