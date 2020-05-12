using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteractionScript : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas, gameCam;

    void Start()
    {
        canvas.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvas.SetActive(true);
            Quaternion tempRot = canvas.transform.rotation;
            canvas.transform.LookAt(gameCam.transform.position);

            if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                Destroy(this.gameObject);
                canvas.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
}

