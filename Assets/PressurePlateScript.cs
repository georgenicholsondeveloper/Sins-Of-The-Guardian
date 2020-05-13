using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField]
    GameObject bridgeManager;

    [SerializeField]
    float cooldown;

    [SerializeField]
    bool shouldCooldown, active;

    [SerializeField]
    int Bridge1, Bridge2;

    private void Update()
    {
        BridgeRotate();
    }

    void BridgeRotate()
    {
        if (active)
        {
            if (Time.time < cooldown)
            {
                bridgeManager.GetComponent<BridePuzzleManagerScript>().interactionNumber = Bridge1;
                bridgeManager.GetComponent<BridePuzzleManagerScript>().secondInteraction = Bridge2;
            }
            else
            {
                bridgeManager.GetComponent<BridePuzzleManagerScript>().interactionNumber = 0;
                bridgeManager.GetComponent<BridePuzzleManagerScript>().secondInteraction = 0;
                active = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blockade")
        {
            active = true;
            cooldown = Time.time + cooldown;
        }
    }
}
