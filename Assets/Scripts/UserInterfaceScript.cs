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
                    print("HELLO");
                    tutorialText.enabled = true;
                }
                else
                    tutorialText.enabled = false;
                break;
        }

    }
}
