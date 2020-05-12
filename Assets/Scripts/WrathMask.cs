using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathMask : MonoBehaviour
{
    public bool aiming;

    private bool activateAbility;
    [SerializeField]
    private ParticleSystem wrathParticle, hitParticle, chargedParticle;
    private List<ParticleSystem> particles = new List<ParticleSystem>();
    private float countdown, timeHeld;
    private List<GameObject> hitObjects = new List<GameObject>();


    private void Start()
    {
        wrathParticle.Stop(); //Disable the Particle Effect Tied to the Player's Hand.
        chargedParticle.gameObject.SetActive(false);
    }

    void Update()
    {
        Aiming(); //Check if aiming.
        DetectHit(); //Use Wrath Ability.
        DestroyParticle(); //Destroy Cloned Particles.
        Cooldown(); //Calculate Time Until Ability Refreshed.
    }


    void DetectHit()
    {
        if (activateAbility && countdown <= 0 && (Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKeyUp(KeyCode.Joystick1Button2))) //If the mask's ability should activate and the cooldown has been reached.
        {
            timeHeld += Time.unscaledDeltaTime;

            if (timeHeld >= 1.5f)
                chargedParticle.gameObject.SetActive(true);

              

            if (Input.GetKeyUp(KeyCode.Joystick1Button2))
            {
                if (timeHeld > 1.5f)
                {
                    Collider[] sphereHits = Physics.OverlapSphere(transform.position, 10f);

                    foreach(Collider c in sphereHits)
                    {
                        if (c.GetComponent<Rigidbody>() && c.tag != "MainCamera" && c.transform.gameObject.GetComponent<Collider>())
                        {
                            hitObjects.Add(c.gameObject);
                            ApplyForce();
                        }
                    }
                }
                else
                {
                    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward * 25f, out RaycastHit hit2))
                    {
                        //Check Raycast from the camera's forward direction for 50 meters and return if it collides with anything.
                        if (hit2.transform.gameObject.GetComponent<Rigidbody>() && hit2.transform.gameObject.GetComponent<Collider>() && !hit2.transform.gameObject.GetComponent<Collider>().isTrigger) //Is the collision a rigidbody?
                        {
                            hitObjects.Add(hit2.transform.gameObject);
                            ApplyForce();
                        }
                    }

                }
                    timeHeld = 0;
                    wrathParticle.Play(); //Activate the Wrath Particle on the Hand.
                    chargedParticle.gameObject.SetActive(false);
                    activateAbility = false; //Disable the Ability.
                }
            }
        }
    

    void ApplyForce()
    {
        countdown = 1; //Set cooldown to 1.

        float force = 0;

        foreach (GameObject hit in hitObjects)
        {
            if (hit.GetComponent<Collider>().bounds.size.sqrMagnitude > 2000) //Apply Different Force relative to Object's Size.
                force = 10000 / (hit.transform.gameObject.GetComponent<Renderer>().bounds.size.sqrMagnitude / 20);
            else if (hit.GetComponent<Collider>().bounds.size.sqrMagnitude > 100)
                force = 10000 / (hit.transform.gameObject.GetComponent<Renderer>().bounds.size.sqrMagnitude / 50);
            else if (hit.transform.gameObject.GetComponent<Renderer>().bounds.size.sqrMagnitude < 100)
                force = 5000;

            hit.GetComponent<Rigidbody>().velocity = Vector3.zero; //Kill the velocity of the rigidbody (Prevents Momentum buildup).
            hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(((hit.transform.position - transform.position).normalized * force));
            // ^^ Apply force to rigidbody hit.
            particles.Add(Instantiate(hitParticle, hit.transform.position, transform.rotation, null));
            //Instantiate the hit particles at point of collision and add them to a List.
        }
        hitObjects.Clear(); //Clear the List of Objects to prevent them from being hit every time.
    }


    void Cooldown()
    {
        if (countdown > 0) //If countdown is above 0, reduce the countdown by seconds passed.
            countdown -= Time.unscaledDeltaTime;
    }

    void DestroyParticle()
    {
        if (particles.Count > 0) //If there are Wrath Hit particles in the scene.
        {
            for (int i = 0; i < particles.Count; i++) //For each particle.
            {
                if (particles[i].isStopped) //If it has finished.
                {
                    Destroy(particles[i].gameObject); //Destroy the particle.
                    particles.Remove(particles[i]); //Remove it from the List.
                }
            }
        }
    }

    void Aiming()
    {
        if (Input.GetAxis("LeftTrigger") != 0 && !FindObjectOfType<GameManagerScript>().dead)
            aiming = true; //If the Left Trigger is held down and the Player isn't dead, aiming is true.
        else
            aiming = false;
    }

    private void OnGUI()
    {
        //Show Crosshair when Left Trigger is held.
        if (aiming)
        {
            if (!FindObjectOfType<GameManagerScript>().dead && !FindObjectOfType<GameManagerScript>().paused)
                GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 3, 3), "");
        }
    }

    public void ActivateAbility(bool shouldActivate) //Invoked from the MaskSelectionScript.
    {
        if (shouldActivate && countdown <= 0) //If the ability should activate and the Cooldown has reached 0.
            activateAbility = true;
        else
            activateAbility = false;
    }
}


