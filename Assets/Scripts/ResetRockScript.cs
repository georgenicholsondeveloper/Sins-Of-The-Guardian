using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRockScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Blockade")
        {
            if (other.GetComponent<FallingRockScript>())
            {
                other.GetComponent<FallingRockScript>().reset = true;
            }
        }
    }
}
