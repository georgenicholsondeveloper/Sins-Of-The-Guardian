using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockTriggerScript : MonoBehaviour
{
    [SerializeField]
    GameObject rock;
    private void Start()
    {
        rock.GetComponent<Rigidbody>().isKinematic = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Blockade")
            rock.GetComponent<Rigidbody>().isKinematic = false; 
    }
}
