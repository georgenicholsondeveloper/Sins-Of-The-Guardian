using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControlScript : MonoBehaviour
{
    public GameObject cameraOrbit;
    public bool colliding;

    [SerializeField]
    private GameObject gameManager;
    private float rotateSpeed, v, h, cooldown;
    private Vector3 targetRot, orbitSize;
    
    void Update()
    {
        if (!gameManager.GetComponent<GameManagerScript>().paused && !FindObjectOfType<DamageScript>().dead)
        {
            RotateCamOrbit(); //Rotate the Camera.
            Aiming(); //Change Sensitivity if Aiming.
            ScaleOrbit(); //Collision Detection and Reaction.
        }
    }

    private void Start()
    {
        orbitSize = cameraOrbit.transform.localScale; //Get the Orbit Object's original size.
    }

    void ScaleOrbit()
    {
        if (colliding)
        { //If the Camera is colliding with a wall, reduce the size of the Orbit object until it isn't.
            cameraOrbit.transform.localScale *= 0.965f;
            cooldown = Time.time + 0.5f;
        }
        else if (Time.time > cooldown && cameraOrbit.transform.localScale.x <= orbitSize.x) //Once the camera is no longer colliding, scale Orbit Object to it's original size.
            cameraOrbit.transform.localScale /= 0.965f;
        
    }

    void RotateCamOrbit()
    {
        v = Input.GetAxis("JVertical") * rotateSpeed; //Get Vertical and Horizontal Input from RightAnalogueStick.
        h = Input.GetAxis("JHorizontal") * rotateSpeed;

        Vector3 input = new Vector3(h, v, 0); //Establish a Vector three using the input.

        if (input.sqrMagnitude > 0.02f) //If the Player is pushing the stick enough to move the camera. (This prevents floating cam when not pressing down).
        {   //Calculate TargetPosition.
            targetRot = new Vector3(cameraOrbit.transform.eulerAngles.x + input.y, cameraOrbit.transform.eulerAngles.y + h, cameraOrbit.transform.eulerAngles.z); 
            cameraOrbit.transform.eulerAngles = targetRot; //Make the Orbit's rotation the target rotation.
        }
    }

    void Aiming()
    {
        if (FindObjectOfType<WrathMask>().aiming) // If the player is aiming, reduce the sensitivity for more precision.
            rotateSpeed = 15f; 
        else
            rotateSpeed = 20f; 
    }
}


