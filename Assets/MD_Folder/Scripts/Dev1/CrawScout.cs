using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawScout : MonoBehaviour
{
    /*something something to make the zone to move
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent.GetComponent<CrawAI>().SCREAM = true;
            transform.parent.GetComponent<CrawAI>().PlayerPos = other.transform;
        }
    }
}
