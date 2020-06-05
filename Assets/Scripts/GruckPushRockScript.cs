using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruckPushRockScript : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;
    private GameObject gameCam;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponentInChildren<Camera>())
            {
                gameCam = other.GetComponentInChildren<Camera>().gameObject;
                canvas.SetActive(true);
                canvas.transform.LookAt(gameCam.transform.position);

                if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                    transform.parent.GetComponent<Rigidbody>().isKinematic = false;
            }          
        }
   
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            canvas.SetActive(false);
        }
    }
}
