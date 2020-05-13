using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockScript : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    public bool reset;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = startPos;
            transform.rotation = startRot;
            GetComponent<Rigidbody>().isKinematic = false;
            reset = false;
        }
    }
}
