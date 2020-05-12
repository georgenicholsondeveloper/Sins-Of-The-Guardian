using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public bool paused, dead;

    private float currentTimeScale;


    void Update()
    {
        InputManager();  //Checks for Player Input.
    }

    void InputManager()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) && !player.GetComponent<DamageScript>().dead)
        { // ^^ If the player presses 'Start', and they aren't dead.
            if (!paused) //If the game isn't paused already.
                paused = true;
            else
                paused = false;
        }
    }

    void PauseGame()
    {
        if (paused) //If the game should pause.
        {
          //  paused = true; //Set the public pause variable to true.
            currentTimeScale = Time.timeScale; //Store the current time scale (May not be One due to Sloth).
            Time.timeScale = 0; //Set the timescale to 0.
        }
        else
        {
          //  paused = false; //Set the public pause variable to false.
            Time.timeScale = currentTimeScale; //Restore the timeScale to it's value before pause.
        }
    }
}
