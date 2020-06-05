using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceScript : MonoBehaviour
{
    [SerializeField]
    GameObject maskSelectionSystem, pauseScreenElements, deathScreenElements;
    [SerializeField]
    Image slothUi;
    [SerializeField]
    Text tutorialText;

    public GameObject gameManager;

    public bool slothActive = false, tutorial;
    bool isPaused, isDead;
 

    private void Start()
    {
        tutorialText.enabled = false;
    }

    void Update()
    {
        GamePaused(); //Activate Pause Screen UI Elements.
        PlayerDead(); //Activate Death Screen UI Elements.
        MaskSelectionElements(); //Activate Mask Selection UI Elements.
        SlothActive(); //Activate Sloth Ability UI Elements.
    }

    void GamePaused()
    {
        isPaused = gameManager.GetComponent<GameManagerScript>().paused; //Get whether player has Paused game.

        if (isPaused && !tutorial)
            pauseScreenElements.SetActive(true); //Activate if paused.
        else
            pauseScreenElements.SetActive(false); //Disable if not paused.
    }

    void PlayerDead()
    {
        isDead = gameManager.GetComponent<GameManagerScript>().dead; //Get whether player has Died.

        if (isDead)
            deathScreenElements.SetActive(true); //Activate if dead.
        else
            deathScreenElements.SetActive(false); //Disable if alive.
    }

    void MaskSelectionElements()
    {
        if(isPaused || isDead) //If the game is paused or the player has died, disable Mask UI.
            maskSelectionSystem.SetActive(false);
        else
            maskSelectionSystem.SetActive(true); //Activate if not true.
    }


    void SlothActive()
    {
        if (slothActive) //If the player has used the Sloth Ability.
        {
            slothUi.enabled = true; //Activate the UI element.
            RectTransform rectTransform = slothUi.GetComponent<RectTransform>();
            rectTransform.Rotate(new Vector3(0, 0, 10 * Time.unscaledDeltaTime)); //Rotate the image to create the effect.
        }
        else
            slothUi.enabled = false; //Disable the UI element.
    }

    public void Tutorial(int tutorialNumber, bool completed)
    {
        switch (tutorialNumber)
        {
            case 1:
                if (!completed)
                {
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
            case 2:
                if (!completed)
                {
                    tutorialText.text = "Use the Right Stick to Look Around";
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
            case 3:
                if (!completed)
                {
                    tutorialText.text = "Jump with the 'A' button. Press it Twice to Double Jump";
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
            case 4:
                if (!completed)
                {
                    tutorialText.text = "This is a fountain. If the Guardian becomes too damaged he will return here. Activate it by getting close and pressing the 'Y' button." + "\n" + "\n" +  "Press 'B' to continue.";
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
            case 5:
                if (!completed)
                {
                    tutorialText.text = "The Sloth Mask Slows time for 5 seconds. Press 'X' to activate it";
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
            case 6:
                if (!completed)
                {
                    tutorialText.text = "Use LB and RB to cycle through the masks";
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
            case 7:
                if (!completed)
                {
                    tutorialText.text = "The Pride Mask allows you to blink forward" + "\n" + "Select the Circular Mask and Press 'X' to activate it";
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
            case 8:
                if (!completed)
                {
                    tutorialText.text = "You can combine abilities if you're quick enough. Try using Sloth and Pride to cross this gap" + "\n" + "Press 'B' to continue";
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
            case 9:
                if (!completed)
                {
                    tutorialText.text = "The Lust Mask allows you to control corrupted inhabitants."+ "\n"  + "\n" + "Select the Wrapped Mask and Aim at the Roc-Hound with the Left Trigger" + "\n"+ "Press 'X' to activate it";
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
            case 10:
                if (!completed)
                {
                    tutorialText.text = "Puzzles can be solved in a variety of ways using your abilities." + "\n" + "This area has two different ways you can progress" + "\n" + "Press 'B' to Continue";
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
            case 11:
                if (!completed)
                {
                    tutorialText.text = "The Wrath Mask shoots a strong force from the Guardian's hand." + "\n" + "Select the Horned Mask, use Left Trigger to aim and Press 'X' to activate it" + "\n" + "\n" + "Try aiming it at this rock, and use Sloth to ride it on the way down";
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
            case 12:
                if (!completed)
                {
                    tutorialText.text = "This area is Corrupted!" + "\n" + "You must find the corrupted node and destroy it to cleanse the area." + "\n" + "Press 'B' to Continue";
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;

        }

    }
}
