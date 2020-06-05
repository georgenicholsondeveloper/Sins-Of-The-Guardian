using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField]
    GameObject activeLightBall, deactiveLightball, canvas;
    [SerializeField]
    GameObject spawnPoint, gameCam;

    private GameObject lastCheckpoint;

    private void Start()
    {
        activeLightBall.GetComponent<ParticleSystem>().Stop(); //Deactivate Particle System.
        canvas.SetActive(false); //Deactivate Canvas.
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button3)) //If the Player presses 'Y' within the trigger.
            {
                lastCheckpoint = other.GetComponent<DamageScript>().lastCheckpoint; //Get the currently set checkpoint from the player.

                if (lastCheckpoint) //If there is a previous checkpoint alocated to the Player.
                {
                    other.GetComponent<DamageScript>().lastCheckpoint = activeLightBall; //Set the Player's last Checkpoint to this.
                    lastCheckpoint.GetComponent<ParticleSystem>().Stop(); //Stop the currently stored last checkpoint's primary particle.
                    lastCheckpoint.GetComponent<Renderer>().enabled = false; //Disable the currently stored last checkpoint's renderer.
                    lastCheckpoint.GetComponentInChildren<ParticleSystem>().Play(); //Activate the secondary particle of the last checkpoint.
                }
                else
                    other.GetComponent<DamageScript>().lastCheckpoint = activeLightBall; //Set the player's checkpoint here.


                other.GetComponent<DamageScript>().spawnPosition = spawnPoint.transform.position; //Set the Player's spawn position and rotation to here.
                other.GetComponent<DamageScript>().spawnRotation = new Quaternion(other.transform.rotation.x, spawnPoint.transform.rotation.y, other.transform.rotation.z, 1);
                activeLightBall.GetComponent<ParticleSystem>().Play(); //Activate the primary particle effect.
                activeLightBall.GetComponent<Renderer>().enabled = true; //Enable the renderer.
                deactiveLightball.GetComponent<ParticleSystem>().Stop(); //Stop the secondary particle effect. (It's a child so becomes active)
                lastCheckpoint = activeLightBall; //Set the locally stored last checkpoint to this one.
            }

            ActivateUI(); //Activate the UI elements when player is within Trigger.
        }
    }

    void ActivateUI()
    {
        if (activeLightBall.GetComponent<ParticleSystem>().isStopped || deactiveLightball.GetComponent<ParticleSystem>().isPlaying)
        {
            canvas.SetActive(true); //Activate the canvas.
            canvas.transform.LookAt(gameCam.transform.position); //Canvas looks at the camera.
        }
        else
            canvas.SetActive(false); //Deactivate canvas.
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            canvas.SetActive(false); //When player leaves trigger, deactivate UI Elements.
    }
}
