using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogReverseScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<ThirdPersonCamScript>().isInFog = true;   
        }          
    }
}
