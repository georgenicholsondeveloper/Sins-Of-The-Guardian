using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothMask : MonoBehaviour
{
    [SerializeField]
    GameObject userInterface;

    public float SlowTime = 0.1f;
    public float NormalTime = 1f;
    public float SlothDuration = 5f;
    public float SlothCooldown = 0f;
    public bool DoSlowTime = false, activateAbility = false;

    void Update()
    {
        HingeJoint[] joints = HingeJoint.FindObjectsOfType<HingeJoint>();

        if (DoSlowTime == false && activateAbility == true && SlothCooldown <= 0f)
        {
            Time.timeScale = SlowTime;
            DoSlowTime = true;
            userInterface.GetComponent<UserInterfaceScript>().slothActive = true;
        }
        else if (DoSlowTime == true && activateAbility == false || SlothDuration <= 0f)
        {
            Time.timeScale = NormalTime;
            DoSlowTime = false;
            userInterface.GetComponent<UserInterfaceScript>().slothActive = false;
            SlothDuration = 5f;
            SlothCooldown = 5f;
        }

        if (DoSlowTime == true)
        {
           SlothDuration = SlothDuration - Time.unscaledDeltaTime;


            foreach (HingeJoint i in joints)
                i.breakForce = 8000;
        }
        if (DoSlowTime == false && SlothCooldown >= 0 || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            foreach (HingeJoint i in joints)
                i.breakForce = 1000;

            SlothCooldown = SlothCooldown -= Time.deltaTime;
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
