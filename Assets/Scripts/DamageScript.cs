using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public bool dead = false;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
    public int health = 100;
    public GameObject lastCheckpoint;

    [SerializeField]
    private GameObject Model, gourdParticleOne, gourdParticleTwo;
    private bool playAnimation;
    private ParticleSystem.MainModule gourdOne;
    private ParticleSystem.MainModule gourdTwo;

    private void Start()
    {
        spawnPosition = transform.position; //Set the Spawn Position to the place the player starts.
        gourdOne = gourdParticleOne.GetComponent<ParticleSystem>().main; //Set the Particle System's Main Modules.
        gourdTwo = gourdParticleTwo.GetComponent<ParticleSystem>().main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) && dead) //If the player presses 'Start' and are Dead.
            Respawn(); //Handles the Respawning of the Player.

        Death(); //Handles the Death of the Player.
        Animation(); //Handles the Death Animation.
    }

    public void Damage(int dmg) // Recieve an integer representing Damage from an enemy.
    {
        health -= dmg; //Reduce the player's Health by the integer Parsed.
        gourdOne.startColor = Color.red; //Set the Gourd Particles to Red.
        gourdTwo.startColor = Color.red;
    }

    void Death()
    {
        if (health <= 0 && !dead) //If health reaches 0 and the player is still alive.
        {
            playAnimation = true; //Play death Animation.
            dead = true; //Deactivate Scripts by setting dead to true.
            FindObjectOfType<GameManagerScript>().dead = true; //Activate Death Screen.
        }
    }

    public void Respawn()
    { 
        FindObjectOfType<GameManagerScript>().dead = false; //Disable the Death Screen.
        transform.position = spawnPosition; //Set Position to the Spawn Position;
        Model.transform.position = new Vector3(transform.position.x,transform.position.y -1.17f,transform.position.z); 
        // ^^ Set the Model's Position.
        transform.rotation = spawnRotation; //Set the rotation of the player.
        Model.transform.rotation = transform.rotation; //Set the rotation of the model.
        gourdOne.startColor = Color.cyan; //Return the Gourd Particles to normal colour.
        gourdTwo.startColor = Color.cyan;
        transform.GetComponentInChildren<Animator>().Play("Idle", 0); //Return the animator to Idle.
        health = 100; //Set Player's Health to 100.
        dead = false; //Reactivate Scripts by setting dead to false.
    }

    void Animation()
    {
        bool ani = false;

        if (playAnimation == true) //Switch boolean to only play animation Once.
        {
            ani = true;
            playAnimation = false;
        }
        else
            ani = false;

        transform.GetComponentInChildren<Animator>().SetBool("IsDead", ani);
    }
}
