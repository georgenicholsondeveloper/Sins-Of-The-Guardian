using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterScript : MonoBehaviour
{
    public float jogSpeed, sprintSpeed, jumpForce, gravity;
    public bool attacking = false, lockedOn = false, isClimbing = false;
    public Vector3 rotationSpeed, move;
    public Camera gameCam;
    public GameObject trackPotential;
    public Animator animator;

    private CharacterController player;
    private Quaternion deltaRotation;
    private Vector3 hitNormal;
    private float  direction = 0f, directionSpeed = 3f, rotSpeed, moveSpeed, slopeLimit = 60f;
    private bool jumping, doubleJump, sliding;


    void Start()
    {
        player = GetComponent<CharacterController>();     
    }

    void Update()
    {
        StickToWorldspace(this.transform, trackPotential.transform, ref direction, ref rotSpeed); //Recieve direction and speed from Analog Sticks.
        Animation(rotSpeed, direction); //Parse direction and speed to animator
    
        InputController();
        Jump();

        if (FindObjectOfType<ThirdPersonCamScript>().isInFog)
            transform.Rotate(0, 180, 0);

        Movement();

    }

     bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, GetComponent<CapsuleCollider>().bounds.extents.y + 0.12f);
    }

    void InputController()
    {
        if (Input.GetKey(KeyCode.Joystick1Button8) || Input.GetKey(KeyCode.LeftShift))
            moveSpeed = sprintSpeed;
        else
            moveSpeed = jogSpeed;               
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    private void OnGUI()
    {
        if (Input.GetAxis("LeftTrigger") != 0 || Input.GetKey(KeyCode.Mouse1))
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 3, 3), "");
        }
    }

    void Jump()
    {
         if (Input.GetKeyDown(KeyCode.Joystick1Button0) && player.isGrounded || Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            gravity = 40;
            jumping = true;
        }
        else if(Input.GetKeyDown(KeyCode.Joystick1Button0) && !doubleJump || Input.GetKeyDown(KeyCode.Space) && !doubleJump)
        {
            jumping = true;
            doubleJump = true;
        }
        else
        {
            jumping = false;
        }

        if (player.isGrounded)
        {
            if (move.y != 0)
                move.y = 0;

            gravity = 40;
            doubleJump = false;
        }

        if (jumping)
        {
            move.y = jumpForce;
        }
    }


    void Movement()
    {
        Vector3 moveDirection = transform.forward * rotSpeed * moveSpeed * Time.unscaledDeltaTime;


            move.x = moveDirection.x * moveSpeed;
            move.z = moveDirection.z * moveSpeed;


        if (!(Vector3.Angle(Vector3.up, hitNormal) <= slopeLimit))
            sliding = true;
        else
            sliding = false;

        move.y -= gravity * Time.unscaledDeltaTime;   
        

        player.Move(move * Time.unscaledDeltaTime);

        deltaRotation = Quaternion.Euler(direction * rotationSpeed);
        
        player.transform.rotation = player.transform.rotation * deltaRotation;
    }



    void Animation(float aniSpeed, float aniDirection)
    {
        if (animator)
        {
            animator.SetFloat("Speed", aniSpeed);
            animator.SetFloat("Direction", aniDirection);
            animator.SetBool("Jump", jumping);
        //    animator.SetBool("Attacking", attacking);
          //  animator.SetBool("IsClimbing", isClimbing);
        }
    }

    void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
    {
        Vector3 rootDirection = root.forward;
        Vector3 stickDirection = new Vector3(Input.GetAxis("Horizontal") * Time.unscaledDeltaTime, 0, Input.GetAxis("Vertical") * Time.unscaledDeltaTime) * 1000; // This is the 

        if (stickDirection.sqrMagnitude > 1)
            stickDirection.Normalize();

        speedOut = stickDirection.sqrMagnitude; //Calculate the value of the Left Analogue Stick (Where and How far it is being pressed)

        Vector3 cameraDirection = camera.forward;
        cameraDirection.y = 0f;
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection); 

        Vector3 moveDirection = referentialShift * stickDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

        float angelRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

        angelRootToMove /= 20f;

        directionOut = angelRootToMove * directionSpeed;
    }

  
}
