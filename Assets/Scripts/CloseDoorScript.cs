using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorScript : MonoBehaviour
{
    bool closing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            closing = true;
        }

        if (closing)
        {
            if(transform.name.Contains("27"))
                transform.Translate(-Vector3.right * 5 * Time.deltaTime);
            else
                transform.Translate(-Vector3.right * 5 * Time.deltaTime);
        }
    }
}
