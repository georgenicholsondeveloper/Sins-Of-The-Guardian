using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacterScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameManager;

    public float jogSpeed, sprintSpeed, jumpForce, gravity;
    public Vector3 rotationSpeed, move;
    public Camera gameCam;
    public GameObject trackPotential;
    public Animator animator;

    private CharacterController player;
    private CapsuleCollider capCollider;
    private Quaternion deltaRotation;
    private float direction = 0f, directionSpeed = 3f, rotSpeed, moveSpeed;
    private bool jumping, doubleJump, landed, grounded;

    void Start()
    {
        player = GetComponent<CharacterController>();
        capCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (!GetComponent<DamageScript>().dead && !gameManager.GetComponent<GameManagerScript>().paused 
            && Time.timeSinceLevelLoad > 0.4f && !FindObjectOfType<LustMask>().lustActive)
        {
            StickToWorldspace(this.transform, trackPotential.transform, ref direction, ref rotSpeed); //Recieve direction and speed from Analog Sticks.
            Animation(rotSpeed, direction, jumping); //Parse direction, speed and jump to animator.
            InputController();  //Take Player Input and Output Accordingly.
            Movement(); //Handle player movement (Player Speed, Rotation and Gravity).
        }
    }

    private void FixedUpdate()
    {
        IsGrounded(); // More accurate IsGrounded check, characterController.isGrounded was not functional.
    }

    void InputController()
    {
        if (Input.GetKey(KeyCode.Joystick1Button8))// If Left Stick is Clicked in and Held (L3), make player speed sprint, else make them jog.
            moveSpeed = sprintSpeed;         
        else
            moveSpeed = jogSpeed;

        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) //If Player presses A. Call jump function.
            Jump(true);
        else
            Jump(false);
        
    }

    void IsGrounded()
    {     
        if (Physics.SphereCast(transform.position, capCollider.radius + 0.5f, -transform.up, out RaycastHit hit)) //Cast a Sphere down slightly wider than player collider.
        {
            if (!hit.collider.isTrigger && hit.distance <= 0.3f) //If the collider it hits isn't a trigger (Fog), and the distance is close, ground the player.
                grounded = true;
            else
                grounded = false;
        }
        else
           grounded = false;       
    }

    void Jump(bool pressedJump)
    {
        if (pressedJump) //If the player jumps whilst grounded.
        {
            if (grounded || player.isGrounded)
            {
                grounded = false;
                jumping = true;
            }
            else if (!doubleJump)//If the player jumps in the air and they haven't double jump yet.
            {
                grounded = false;
                jumping = true;
                doubleJump = true;
            }
        }

        if (jumping) //Apply the jumpforce to the player, Tell the Animator to play the jump animation.
        {
            grounded = false;
            move.y = jumpForce;
            Animation(rotSpeed, direction, true);
            jumping = false;
        }
    }

    void Movement()
    {
        Vector3 moveDirection = transform.forward * rotSpeed * moveSpeed * Time.unscaledDeltaTime; //Calculate the direction to Move.

        move.x = moveDirection.x * moveSpeed;
        move.z = moveDirection.z * moveSpeed; //Multiply direction by moveSpeed.

        if (grounded || player.isGrounded) //{If grounded set y direction to -5, allowing small descent without it multiplying. --
        {                        // -- Set double jump to false, allowing the reset for the jump function.}
            if (!landed)
            {
                move.y = -5f;
                landed = true;
            }
            doubleJump = false;
        }
        else //If they aren't grounded, apply normal gravity.
        {
            move.y -= gravity * Time.unscaledDeltaTime;
            landed = false;
        }
        

        player.Move(move * Time.unscaledDeltaTime); //Move the player by the Vector3

        deltaRotation = Quaternion.Euler(direction * rotationSpeed);      
        player.transform.rotation = player.transform.rotation * deltaRotation; //Rotate the player with the analogue sticks.
    }

    void Animation(float aniSpeed, float aniDirection, bool jumping)
    {
        if (animator) //Set the variables of the Animator.
        {
            animator.SetFloat("Speed", aniSpeed);
            animator.SetFloat("Direction", aniDirection);
            animator.SetBool("Jump", jumping);
        }
    }

    void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
    {
        Vector3 rootDirection = root.forward;
        Vector3 stickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Get Stick Direction ^^.

        if (stickDirection.sqrMagnitude > 1) //Normalise the Stick Direction
            stickDirection.Normalize();

        speedOut = stickDirection.sqrMagnitude; //Calculate the value of the Left Analogue Stick (Where and How far it is being pressed)

        Vector3 cameraDirection = camera.forward;
        cameraDirection.y = 0f;
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection); //Get the angle between the camera'sforward vector and the worldSpace's.

        Vector3 moveDirection = referentialShift * stickDirection; //Get the Direction the Player is pushing the Sticks in Worldspace.
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection); //Get the Crossproduct of the moveDirection and the RootDirection.

        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f); //Get the angle to rotate the player.

        angleRootToMove /= 10f; //Apply an offset to allow the Player to rotate around a smaller angle.

        directionOut = angleRootToMove * directionSpeed; //Output the direction the player should face.
    }

}
