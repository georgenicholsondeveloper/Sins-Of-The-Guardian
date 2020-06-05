using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrideMask : MonoBehaviour
{
    private Vector3 blinkTo;
    private bool activateAbility;
    private float dashSpeed = 5f, prideDelay;
    [SerializeField]
    GameObject dashParticle;

    [SerializeField]
    Transform cameraOrbit;

    private void Start()
    {
        dashParticle.SetActive(false); //Disable the Dash particle effect.
    }

    private void Update()
    {
        UseDash(); //Check if the Dash should be used.
    }

    void UseDash()
    {
        if (activateAbility && prideDelay <= 0) //If the ability has been used and the cooldown has been reached.
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 10f)) //Check if there is a wall in the way.
                blinkTo = transform.position + transform.forward * (hit.distance - 2f); //End the dash before the wall. (Collision Detection).
            else
                blinkTo = transform.position + transform.forward * 10; //Dash 10 Metres.

            prideDelay = 1; //Set Cooldown to 1.
            dashParticle.SetActive(true); //Activate Dash Particle.
    
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false; //Disable the Player's Mesh Renderer to stop Screen tear effect.

            for (float t = 0; t < 1f; t += Time.deltaTime / dashSpeed) //Dash Over Time.
            {
                transform.GetComponent<CharacterController>().enabled = false; //Disable Character Controller.
                transform.position = Vector3.Lerp(transform.position, blinkTo, 0.1f);
                cameraOrbit.position = Vector3.Lerp(transform.position, blinkTo, 0.1f);//Move Player and Camera to dash distance.
                transform.GetComponent<CharacterController>().enabled = true; //Reactivate Character Controller.
            }
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            activateAbility = false; //Disable activateAbility.
        }
        else if(prideDelay > 0) //If the cooldown is greater than 0.
        {
            prideDelay = prideDelay -= Time.unscaledDeltaTime; //Reduce Cooldown by Time.
        }

        if(prideDelay < 0.5f)
        {
            dashParticle.SetActive(false); //Once the Particle has been awake for 0.5 seconds, disable it.
        }

        if (prideDelay <= 0 && !GetComponent<MaskSelection>().abilityRefreshed) //If the cooldown has been reached, reactivate ability refreshed on Mask Selection 
            GetComponent<MaskSelection>().abilityRefreshed = true;//(This stops multiple button presses activating the ability).
    }

    public void ActivateAbility(bool shouldActivate) //Activate The Ability.
    {
        if (shouldActivate)
            activateAbility = true;
        else
            activateAbility = false;
    }
}
