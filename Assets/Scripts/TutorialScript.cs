using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private GameObject Ui;
    private UserInterfaceScript uiScript;
    private bool completed;

    public bool freezeTime, input;
    public int tutNumber;


    void Start()
    {
        Ui = GameObject.FindGameObjectWithTag("UserInterface");
        uiScript = Ui.GetComponent<UserInterfaceScript>();
    }

    private void Update()
    {
        TutorialStatus();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!completed)
            {
                print(tutNumber);
                uiScript.Tutorial(tutNumber, false);
                print("hi");
                if (freezeTime)
                {
                    uiScript.tutorial = true;
                    uiScript.gameManager.GetComponent<GameManagerScript>().paused = true;
                }
            }
        }
    }

    void TutorialStatus()
    {
        if (!completed)
        {
            if (input)
            {
                uiScript.Tutorial(tutNumber, true);
                completed = true;

                if (freezeTime)
                {
                    uiScript.gameManager.GetComponent<GameManagerScript>().paused = false;
                    uiScript.tutorial = false;                 
                }
            }
        }
    }

}
