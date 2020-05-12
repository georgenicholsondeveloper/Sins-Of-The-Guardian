using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamScript : MonoBehaviour
{
    [SerializeField]
    private Transform followStart, followEnd;

    [SerializeField]
    private GameObject inputControl, gameManager;

    private Vector3 characterOffset = Vector3.zero;

    public bool isInFog;
    public Transform cameraOrbit, target;

    void Update()
    {   
        CamPosition(); //Check whether to use Over The Shoulder Aiming, adjust Cam Position.
        FogRotation(); //Rotate the camera 180degrees in Fog.
    }

    void CamPosition()
    {
        //If the Player is Aiming, Set the camera to Over the Shoulder Pos.
        if (FindObjectOfType<WrathMask>().aiming && !gameManager.GetComponent<GameManagerScript>().paused) 
            target.position = Vector3.Lerp(target.position, followEnd.position, 2f * Time.unscaledDeltaTime);
        else if (!gameManager.GetComponent<GameManagerScript>().paused) //If not, Set the camera to standard third person position.
            target.position = Vector3.Lerp(target.position, followStart.position, 4f * Time.unscaledDeltaTime);

        cameraOrbit.position = target.position; // Apply the target position to the camera's parent.
        transform.LookAt(target.position); //Make the Camera Look At the target (Slightly above the player's head).
    }

    void FogRotation()
    {
        if (isInFog) //If in the fog, add 180 to the Camera's Parent's X rotation.
        {
            cameraOrbit.transform.rotation = 
                Quaternion.Euler(cameraOrbit.transform.rotation.x + 180f, 
                                 cameraOrbit.transform.rotation.y, 
                                 cameraOrbit.transform.rotation.z);
            isInFog = false;
        }
    }

    private void OnTriggerStay(Collider other) //If the Camera is colliding with an object set colliding to true.
    {
        if(!other.isTrigger)
            inputControl.GetComponent<InputControlScript>().colliding = true;
    }

    private void OnTriggerExit(Collider other) //Once the camera is not longer colliding, set it to false.
    {
        inputControl.GetComponent<InputControlScript>().colliding = false;
    }
}
