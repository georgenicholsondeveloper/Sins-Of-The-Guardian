using UnityEngine;
using System.Collections;

public class Dog_ctrl : MonoBehaviour {

	private Animator anim;
	private CharacterController controller;
	private int battle_state = 0;
	public float speed = 6.0f;
	public float runSpeed = 3.0f;
	public float turnSpeed = 60.0f;
	public float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	private float w_sp = 0.0f;
	private float r_sp = 0.0f;
    public bool Manipulating;
    public GameObject EnemyCamera;

    private float direction = 0f, rotSpeed, directionSpeed = 3f, moveSpeed = 25f;
    private Quaternion deltaRotation;
    private Vector3 move, rotationSpeed = new Vector3(0,0.4f,0);

    GameObject gameManager;


	
	// Use this for initialization
	void Start () 
	{						
		anim = GetComponentInChildren<Animator>();
		controller = GetComponent<CharacterController> ();
		w_sp = speed; //read walk speed
		r_sp = runSpeed; //read run speed
		battle_state = 0;
		runSpeed = 1;
        gameManager = GameObject.Find("GameManager");

	}

    void Update()
    {
        if (Manipulating)
        {
            EnemyCamera.SetActive(true);

            if (!gameManager.GetComponent<GameManagerScript>().paused)
            {
                StickToWorldspace(this.transform, EnemyCamera.transform, ref direction, ref rotSpeed);
                Animation(rotSpeed, direction);


               
                Vector3 moveDirection = transform.forward * rotSpeed * moveSpeed * Time.unscaledDeltaTime;


                move.x = moveDirection.x * moveSpeed;
                move.z = moveDirection.z * moveSpeed;

                controller.Move(move * Time.unscaledDeltaTime);


                deltaRotation = Quaternion.Euler(direction * rotationSpeed);

                transform.rotation = transform.rotation * deltaRotation;
            }

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            EnemyCamera.SetActive(false);
            moveDirection.z = 0;
            moveDirection.x = 0;
            anim.SetFloat("Speed", 0f);
            anim.SetFloat("Direction", 0f);
        }
    }

    void Animation(float aniSpeed, float aniDirection)
    {
        if (anim && Manipulating)
        {
            if(aniSpeed > 1|| aniDirection > 1)
            {
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                anim.SetBool("Idle", false);
            }
           
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

        angelRootToMove /= 10f;

        directionOut = angelRootToMove * directionSpeed;
    }
}



