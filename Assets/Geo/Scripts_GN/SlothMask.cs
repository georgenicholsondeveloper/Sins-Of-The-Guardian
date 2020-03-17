using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothMask : MonoBehaviour
{
    public float SlowTime = 0.1f;
    public float NormalTime = 1f;
    public float SlothDuration = 10f;
    public float SlothCooldown = 0f;
    public bool DoSlowTime = false, activateAbility = false;

    void Update()
    {
        HingeJoint[] joints = HingeJoint.FindObjectsOfType<HingeJoint>();

        if (DoSlowTime == false && activateAbility == true && SlothCooldown <= 0f)
        {
            Time.timeScale = SlowTime;
            DoSlowTime = true;
            print("SlowTime");
        }
        else if (DoSlowTime == true && activateAbility == false || SlothDuration <= 0f)
        {
            Time.timeScale = NormalTime;
            DoSlowTime = false;
            SlothDuration = 10f;
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
