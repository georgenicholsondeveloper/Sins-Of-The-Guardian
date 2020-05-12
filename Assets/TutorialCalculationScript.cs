using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCalculationScript : MonoBehaviour
{
    public int tutorialNum;
    TutorialScript tutorialScript;

    private void Start()
    {
        tutorialScript = GetComponent<TutorialScript>();
    }

    void OnTriggerStay()
    {
        switch (tutorialNum)
        {
            case 1:
                tutorialScript.tutNumber = tutorialNum;
                if (Input.GetAxis("Vertical") > 0.1 || Input.GetAxis("Horizontal") > 0.1)
                {                  
                    tutorialScript.input = true;                   
                }
                break;                   
        }
    }
}
