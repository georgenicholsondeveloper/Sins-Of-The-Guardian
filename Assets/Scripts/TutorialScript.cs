using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private GameObject Ui, player;
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
            player = other.gameObject;
            if (!completed)
            {
                uiScript.Tutorial(tutNumber, false);
                if (freezeTime)
                {
                    uiScript.tutorial = true;
                    player.GetComponent<PlayerCharacterScript>().tutorial = true;
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
                    player.GetComponent<PlayerCharacterScript>().tutorial = false;                    
                    uiScript.tutorial = false;                 
                }
            }
        }
    }

}
