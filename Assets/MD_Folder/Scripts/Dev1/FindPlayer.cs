using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindPlayer : MonoBehaviour //rename to HoundSense
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent.GetComponent<TargetPlayer>().target = other.gameObject.transform; //player detected
            //raycasring from player insdead a
            print("Anything");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent.GetComponent<TargetPlayer>().target = other.gameObject.transform; //player detected
            print("Anything");
        }
        if (other.CompareTag("Enemy"))
        {
            if (!other.gameObject.GetComponent<TargetPlayer>().chilling) //enemy detected player
            {
                 transform.parent.GetComponent<TargetPlayer>().target = other.gameObject.GetComponent<TargetPlayer>().target;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print(other.name + " exited from " + transform.parent.name);
        if (other.CompareTag("Player"))
        {
            transform.parent.GetComponent<TargetPlayer>().target = null; // player escaped
        }    
    }
}
