using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRockScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Blockade")
            other.GetComponent<FallingRockScript>().reset = true;
    }
}
