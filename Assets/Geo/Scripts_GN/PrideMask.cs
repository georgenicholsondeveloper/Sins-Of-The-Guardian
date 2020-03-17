using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrideMask : MonoBehaviour
{
    private Vector3 blinkTo;
    private bool activateAbility;
    private float dashSpeed = 5f;

    private void Update()
    {
        UseDash();
     
    }

    void UseDash()
    {
        if (activateAbility)
        {
            blinkTo = transform.position + transform.forward * 10;
            print("Blink");

            for (float t = 0; t < 1f; t += Time.deltaTime * dashSpeed)
            {
                transform.GetComponent<CharacterController>().enabled = false;
                transform.position = Vector3.Lerp(transform.position, blinkTo, t);
                transform.GetComponent<CharacterController>().enabled = true;
            }

            activateAbility = false;
        }
    }

    public void ActivateAbility(bool shouldActivate)
    {
        if (shouldActivate)
            activateAbility = true;
        else
            activateAbility = false;
    }
}
