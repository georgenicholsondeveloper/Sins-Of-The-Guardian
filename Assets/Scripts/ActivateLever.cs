using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLever : MonoBehaviour
{
    bool isActive;

    [SerializeField]
    private GameObject canvas, gameCam;
    [SerializeField]
    private bool shouldCooldown;
    [SerializeField]
    private float cooldown;
    private float addedCooldown;

    private bool shouldRotate;

    [SerializeField]
    GameObject bridgeOne, bridgeTwo;

    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && shouldCooldown)
        {
            if(Time.time >= addedCooldown)
            {
                GetComponentInParent<Animator>().SetBool("PullLever", false);
                shouldRotate = false;
                isActive = false;
            }
        }

        BridgeRotate();
    }

    void BridgeRotate()
    {
        if (shouldRotate)
        {
            bridgeOne.transform.Rotate(0, 15 * Time.deltaTime,0 );
            bridgeTwo.transform.Rotate(0, 15 * Time.deltaTime,0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isActive)
            {
                canvas.SetActive(true);
                Quaternion tempRot = canvas.transform.rotation;
                canvas.transform.LookAt(gameCam.transform.position);

                if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                {
                    GetComponentInParent<Animator>().SetBool("PullLever", true);
                    canvas.SetActive(false);
                    shouldRotate = true;
                    addedCooldown = Time.time + cooldown;
                    isActive = true;                   
                }
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
}
