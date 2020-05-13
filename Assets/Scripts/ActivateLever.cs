using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLever : MonoBehaviour
{
    bool isActive;
    int activNum =0;
  
    [SerializeField]
    private GameObject canvas, gameCam;
    [SerializeField]
    GameObject bridgeManager;
    [SerializeField]
    private bool shouldCooldown;
    [SerializeField]
    private float cooldown, alternateCooldown;

    public bool bridgePuzzle;

    private float addedCooldown;
    private bool shouldRotate;


  

    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && shouldCooldown)
        {
            if (activNum >= 0)
            {
                cooldown = alternateCooldown;
            }
            if(Time.time >= addedCooldown)
            {
                GetComponentInParent<Animator>().SetBool("PullLever", false);
                shouldRotate = false;
                isActive = false;
                BridgeRotate(false);
            }
        }    
    }

    void BridgeRotate(bool x)
    {
        if (bridgePuzzle)
        {
            if (x)
                bridgeManager.GetComponent<BridePuzzleManagerScript>().interactionNumber = int.Parse(transform.name);
            else
                bridgeManager.GetComponent<BridePuzzleManagerScript>().interactionNumber = 0;
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

                if (Input.GetKey(KeyCode.Joystick1Button3))
                {
                    BridgeRotate(true);
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
