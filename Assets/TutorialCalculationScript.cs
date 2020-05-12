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
            switch (tutorialNum)
            {
                case 1:
                    tutorialScript.tutNumber = tutorialNum;
                    if (Input.GetAxis("Vertical") > 0.1 || Input.GetAxis("Vertical") > 0.1)
                    {
                        tutorialScript.input = true;
                    }
                    break;
                case 2:
                    tutorialScript.tutNumber = tutorialNum;


                    rightStick = new Vector2(Input.GetAxis("JVertical"), Input.GetAxis("JHorizontal"));
                    if (rightStick.sqrMagnitude > 1)
                        rightStick.Normalize();
                    
                    if(rightStick.x >= 0.1f || rightStick.x <= -0.1f || rightStick.y >= 0.1f || rightStick.y <= -0.1f)                        
                        tutorialScript.input = true;
                    
                    break;
            }
        }
    }
}
