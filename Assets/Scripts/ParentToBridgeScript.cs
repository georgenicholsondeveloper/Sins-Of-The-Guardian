using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentToBridgeScript : MonoBehaviour
{
    Vector3 startScale;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.SetParent(transform.parent, true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.transform.parent = null;
     //   other.transform.localScale = startScale;
    }
}
