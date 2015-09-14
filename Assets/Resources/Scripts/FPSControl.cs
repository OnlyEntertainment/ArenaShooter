using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class FPSControl : NetworkBehaviour {

    public float speedWalk = 9;
    public float speedRun = 18;
    public bool isRunning = false;
    public float stamina = 100;
    public float staminaRegenerateRate = 3;
    public float staminaUsage = 5;
    private float speedMoving;

    public bool grounded = false;
    public float jumpSpeed = 18;
    public float gravity = 90;
    
    public Vector3 moveDirection = Vector3.zero;
    

    public GameObject playerCameraObject;

    


    //
    // ~~~~~~ Methoden ~~~~~~
    //
    void Start()
    {
        speedMoving = speedWalk;
        
        if (isLocalPlayer)
        {
            playerCameraObject.SetActive(true);
        }
    } // END Start

	
	void Update () 
    {
        if (!isLocalPlayer) return;



        if (grounded == true)
        {

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speedMoving;

            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveDirection.y = jumpSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        CharacterController cController = GetComponent<CharacterController>();
        CollisionFlags flags = cController.Move(moveDirection * Time.deltaTime); // 19 - 2:45

        grounded = (flags & CollisionFlags.CollidedBelow) != 0;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0)
            {
                isRunning = true;
                speedMoving = speedRun;
                StaminaCalculate(true);
            }
            else
            {
                isRunning = false;
                speedMoving = speedWalk;
                StaminaCalculate(false); 
            }
        }
        else
        {
            isRunning = false;
            speedMoving = speedWalk;
            StaminaCalculate(false);
        }

	} // END Update

    void StaminaCalculate(bool running)
    {

        switch (running) 
        {
            case true:

                if (stamina > 0)
                {
                    stamina -= (staminaUsage * Time.deltaTime);

                }
                
                break;
            case false:

                if (stamina < 100)
                {
                    stamina += (staminaRegenerateRate * Time.deltaTime);
                }

                break;
        }

    } // END CheckByRun

} // END Class
