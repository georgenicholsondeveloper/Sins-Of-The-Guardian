using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!other.gameObject.GetComponent<DamageScript>().dead)
            {
                other.GetComponent<DamageScript>().Damage(100);
            }
        }
    }
}
