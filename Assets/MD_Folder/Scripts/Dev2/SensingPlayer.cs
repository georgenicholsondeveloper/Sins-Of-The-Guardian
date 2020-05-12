using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour
{
    private bool pSighted;
    private Vector3 playerPos;
    GameObject eye;
    private float alerteness;

    void Start()
    {
        eye = Instantiate(new GameObject());

        eye.transform.SetParent(gameObject.transform);
        eye.transform.localPosition = new Vector3(0, 0, 0.02f);
    }

    void Update()
    {
        DetectPlayer();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pSighted = true; //Player in sight 
            playerPos = other.transform.position;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPos = other.transform.position;
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
           pSighted = false; // player escaped
        }
    }

    private void DetectPlayer()
    {
        if (pSighted)
        {
            eye.transform.LookAt(playerPos);
            RaycastHit hit;
            if (Physics.Raycast(eye.transform.position, eye.transform.TransformDirection(Vector3.forward), out hit))
            {
                Debug.DrawRay(eye.transform.position, eye.transform.TransformDirection(Vector3.forward) * Vector3.Distance(eye.transform.position, playerPos) , Color.blue);
                print(hit.collider);
                if (hit.collider.CompareTag("Player"))
                {
                    PlayerDetectParameters();
                    print("The y value of eye is: "+ eye.transform.rotation.eulerAngles.y + ", & the distance is: " + Vector3.Distance(eye.transform.position, playerPos)); //14
                }
            }
        }
    }

    private void PlayerDetectParameters()
    {
        alerteness += Time.deltaTime;
        //print(alerteness);
    }

    private void ChasePlayer()
    {
        if (alerteness > 1)
        {

        }
    }
}
