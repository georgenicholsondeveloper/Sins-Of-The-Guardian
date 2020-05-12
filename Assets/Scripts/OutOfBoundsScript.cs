using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsScript : MonoBehaviour
{
    bool executed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            executed = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            if (!other.gameObject.GetComponent<DamageScript>().dead && !executed)
                other.GetComponent<DamageScript>().Damage(100);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            executed = true;
    }
}
