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
    private bool shouldCooldown, infinite;
    [SerializeField]
    private float cooldown, alternateCooldown;
    [SerializeField]
    private int Bridge1, Bridge2;

    public bool bridgePuzzle, bossLever;

    private float addedCooldown;
    private bool shouldRotate, hasSwitchedoff;

    void Start()
    {
        canvas.SetActive(false);
    }

    void Update()
    {
        if (isActive)
        {
            if (activNum >= 0)
            {
                cooldown = alternateCooldown;
            }
            if(Time.time >= addedCooldown)
            {
                if (shouldCooldown)
                {
                    GetComponentInParent<Animator>().SetBool("PullLever", false);
                    shouldRotate = false;
                    isActive = false;
                    if(bridgePuzzle)
                        BridgeRotate(false);
               
                }
                else if (!hasSwitchedoff && !infinite && bridgePuzzle)
                {
                    BridgeRotate(false);
                    hasSwitchedoff = true;
                }
            }
        }    
    }

    void BridgeRotate(bool x)
    {
        if (bridgePuzzle)
        {
            if (x)
            {
                bridgeManager.GetComponent<BridePuzzleManagerScript>().interactionNumber = Bridge1;
                bridgeManager.GetComponent<BridePuzzleManagerScript>().secondInteraction = Bridge2;
            }
            else
            {
                bridgeManager.GetComponent<BridePuzzleManagerScript>().interactionNumber = 0;
                bridgeManager.GetComponent<BridePuzzleManagerScript>().secondInteraction = 0;
            }
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
                    print("hello");
                    BridgeRotate(true);
                    GetComponentInParent<Animator>().SetBool("PullLever", true);
                    canvas.SetActive(false);
                    shouldRotate = true;
                    addedCooldown = Time.time + cooldown;
                    isActive = true;
                    print(bossLever);
                    if (bossLever)
                    {
                        print("HI");
                    }
                }
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
}
