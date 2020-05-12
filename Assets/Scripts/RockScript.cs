using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    bool split;
    MaterialChangeScript m;

    private void Start()
    {
        m = FindObjectOfType<MaterialChangeScript>();  
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "Node")
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
                Instantiate(child, child.position, child.rotation * child.rotation, null);
                child.transform.parent = null;
            }

            m.shrink = true;
            
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
