using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCalculationScript : MonoBehaviour
{
    public int tutorialNum;
    TutorialScript tutorialScript;
    Vector2 rightStick;

    private void Start()
    {
        tutorialScript = GetComponent<TutorialScript>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialScript.tutNumber = tutorialNum;
            switch (tutorialNum)
            {
                case 1:
                    if (Input.GetAxis("Vertical") > 0.1 || Input.GetAxis("Vertical") > 0.1)
                    {
                        tutorialScript.input = true;
                    }
                    break;
                case 2:
                    rightStick = new Vector2(Input.GetAxis("JVertical"), Input.GetAxis("JHorizontal"));

                    if (rightStick.sqrMagnitude > 1)
                        rightStick.Normalize();
                
                    if (rightStick.x >= 0.1f || rightStick.x <= -0.1f || rightStick.y >= 0.1f || rightStick.y <= -0.1f)
                    {
                        tutorialScript.input = true;
                    }                  
                    break;
                case 3:
                    if (Input.GetKey(KeyCode.Joystick1Button0))
                    {
                        tutorialScript.input = true;
                    }
                    break;
                case 4:
                    if (Input.GetKey(KeyCode.Joystick1Button1))
                    {
                        tutorialScript.input = true;
                    }
                    break;
                case 5:
                    if (Input.GetKey(KeyCode.Joystick1Button2))
                    {
                        tutorialScript.input = true;
                    }
                    break;
                case 6:
                    if (Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.Joystick1Button5))
                    {
                        tutorialScript.input = true;
                    }
                    break;
                case 7:
                    if (Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.Joystick1Button5))
                    {
                        tutorialScript.input = true;
                    }
                    break;
                case 8:
                    if (Input.GetKey(KeyCode.Joystick1Button1))
                    {
                        tutorialScript.input = true;
                    }
                    break;
                case 9:
                    if (Input.GetAxis("LeftTrigger") != 0)
                    {
                        tutorialScript.input = true;
                    }
                    break;
                case 10:
                    if (Input.GetKey(KeyCode.Joystick1Button1))
                    {
                        tutorialScript.input = true;
                    }
                    break;
                case 11:
                    if (Input.GetAxis("LeftTrigger") != 0)
                    {
                        tutorialScript.input = true;
                    }
                    break;
                case 12:
                    if (Input.GetKey(KeyCode.Joystick1Button1))
                    {
                        tutorialScript.input = true;
                    }
                    break;
            }
        }
    }
}
