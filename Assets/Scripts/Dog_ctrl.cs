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
		anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController> ();
		w_sp = speed; //read walk speed
		r_sp = runSpeed; //read run speed
		battle_state = 0;
		runSpeed = 1;
        gameManager = GameObject.Find("GameManager");

	}

    // Update is called once per frame
    void Update()
    {
        if (Manipulating)
        {
            EnemyCamera.SetActive(true);

            if (!gameManager.GetComponent<GameManagerScript>().paused)
            {
                StickToWorldspace(this.transform, EnemyCamera.transform, ref direction, ref rotSpeed);
                Animation(rotSpeed, direction);


                /*
                if (Input.GetKey(KeyCode.Alpha1))  // turn to still state
                {
                    anim.SetInteger("battle", 0);
                    battle_state = 0;
                    runSpeed = 1;
                }
                if (Input.GetKey(KeyCode.LeftShift)) // turn to battle state
                {
                    anim.SetInteger("battle", 1);
                    battle_state = 1;
                    runSpeed = r_sp;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    anim.SetInteger("moving", 1);//walk/run/moving
                }
                else
                {
                    anim.SetInteger("moving", 0);
                }


                if (Input.GetKey(KeyCode.S)) //walkback
                {
                    anim.SetInteger("moving", 12);
                    runSpeed = 1;
                    Straight();
                }
                if (Input.GetKeyUp("down"))
                {
                    if (battle_state == 0) runSpeed = 1;
                    else if (battle_state > 0) runSpeed = r_sp;
                }

                if (Input.GetMouseButtonDown(0))
                { // attack1
                    Straight();
                    anim.SetInteger("moving", 2);
                }
                if (Input.GetMouseButtonDown(1))
                { // attack2
                    Straight();
                    anim.SetInteger("moving", 3);
                }
                if (Input.GetMouseButtonDown(2))
                { // attack3
                    Straight();
                    anim.SetInteger("moving", 4);
                }
                if (Input.GetKeyDown("space"))
                { //jump
                    Straight();
                    anim.SetInteger("moving", 6);
                }
                if (Input.GetKeyDown("c"))
                { //roar/howl
                    Straight();
                    anim.SetInteger("moving", 7);
                }

                /*		
                        if (Input.GetKeyDown("p")) // defence_start
                        {
                            anim.SetInteger("moving", 11);
                        }
                        if (Input.GetKeyUp("p")) // defence_end
                        {
                            anim.SetInteger("moving", 12);
                        } 


                if (Input.GetKeyDown("u")) //hit
                {
                    Straight();
                    battle_state = 1;
                    runSpeed = r_sp;
                    anim.SetInteger("battle", 1);

                    int n = Random.Range(0, 2);
                    if (n == 1)
                    {
                        anim.SetInteger("moving", 8);
                    }
                    else
                    {
                        anim.SetInteger("moving", 9);
                    }
                }


                //-------------------------------------------------------------------TURNS

                var vert_modul = Mathf.Abs(Input.GetAxis("Vertical"));
                Debug.Log(vert_modul);

                if ((Input.GetAxis("Horizontal") > 0.1f) && (vert_modul > 0.3f))
                {
                    anim.SetBool("turn_right", true);
                }
                else if (vert_modul > 0.3f)
                {
                    anim.SetBool("turn_right", false);
                }

                if ((Input.GetAxis("Horizontal") < -0.1f) && (vert_modul > 0.3f))
                {
                    anim.SetBool("turn_left", true);
                }
                else if (vert_modul > 0.3f)
                {
                    anim.SetBool("turn_left", false);
                }

                //----------------------------------------------------------------------------------------

                if (Input.GetKeyDown("i"))
                {
                    anim.SetInteger("moving", 13); //die
                    Straight();
                    //		anim.SetBool ("turn_left", false);
                    //		anim.SetBool ("turn_right", false);				
                }
                if (Input.GetKeyDown("o"))
                {
                    anim.SetInteger("moving", 14); //die2
                    Straight();
                    //	anim.SetBool ("turn_left", false);
                    //	anim.SetBool ("turn_right", false);		
                }

            //    if (controller.isGrounded)
             //   {*/
                Vector3 moveDirection = transform.forward * rotSpeed * moveSpeed * Time.unscaledDeltaTime;


                move.x = moveDirection.x * moveSpeed;
                move.z = moveDirection.z * moveSpeed;

                controller.Move(move * Time.unscaledDeltaTime);


                deltaRotation = Quaternion.Euler(direction * rotationSpeed);

                transform.rotation = transform.rotation * deltaRotation;

                // }
            }

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
           // EnemyCamera.SetActive(false);
            moveDirection.z = 0;
            moveDirection.x = 0;
            anim.SetFloat("Speed", 0f);
            anim.SetFloat("Direction", 0f);
        }
    }

    void Straight()
    {
        anim.SetBool("turn_left", false);
        anim.SetBool("turn_right", false);
    }

    void Animation(float aniSpeed, float aniDirection)
    {
        if (anim && Manipulating)
        {
            anim.SetFloat("Speed", aniSpeed);
            anim.SetFloat("Direction", aniDirection);
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



