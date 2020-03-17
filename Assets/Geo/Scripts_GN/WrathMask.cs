using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathMask : MonoBehaviour
{
    public bool aiming;
    private bool activateAbility;

    void Update()
    {
        Aiming();
        UseForce();
    }

    void UseForce()
    {
        if (activateAbility)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward * 100f, out hit))
            {
                if (hit.transform.gameObject.GetComponent<Rigidbody>())
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 10000);
            }

            print("Force");
            activateAbility = false;
        }
    }

    void Aiming()
    {
        if (Input.GetAxis("LeftTrigger") != 0)
            aiming = true;
        else
            aiming = false;
    }

    public void ActivateAbility(bool shouldActivate)
    {
        if (shouldActivate)
            activateAbility = true;
        else
            activateAbility = false;
    }
}


