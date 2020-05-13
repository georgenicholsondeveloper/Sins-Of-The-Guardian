using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruckPuzzleScript : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject canvas;

    private GameObject gameCam;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {

            if (other.gameObject.GetComponentInChildren<Camera>())
            {
                gameCam = other.GetComponentInChildren<Camera>().gameObject;
                canvas.SetActive(true);
                canvas.transform.LookAt(gameCam.transform.position);

                if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                {
                    anim.SetBool("RockPushed", true);
                    Destroy(canvas);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
