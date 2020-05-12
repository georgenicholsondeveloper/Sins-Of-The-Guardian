using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteractionScript : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas, gameCam;

    [SerializeField]
    private GameObject[] corruptedObjects;

    void Start()
    {
        canvas.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag =="Enemy")
        {
            if (other.gameObject.tag == "Enemy")
                gameCam = other.GetComponentInChildren<Camera>().gameObject;

            canvas.SetActive(true);
            Quaternion tempRot = canvas.transform.rotation;
            canvas.transform.LookAt(gameCam.transform.position);

            if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                Cleanse();
                Destroy(this.gameObject);
                canvas.SetActive(false);
            }
        }
    }


    void Cleanse()
    {
        foreach(GameObject i in corruptedObjects)
        {
            i.GetComponent<CleanseScript>().cleansed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
}

