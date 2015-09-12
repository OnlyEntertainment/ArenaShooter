using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class FPSControl : NetworkBehaviour {

    public float speed = 9;
    public float jumpSpeed = 18;
    public Vector3 moveDirection = Vector3.zero;
    public float gravity = 90;
    public bool grounded = false;

    public GameObject playerCameraObject;

    void Start()
    {
        if (isLocalPlayer)
        {
            playerCameraObject.SetActive(true);
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (!isLocalPlayer) return;



        if (grounded == true)
        {

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveDirection.y = jumpSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        CharacterController cController = GetComponent<CharacterController>();
        CollisionFlags flags = cController.Move(moveDirection * Time.deltaTime); // 19 - 2:45

        grounded = (flags & CollisionFlags.CollidedBelow) != 0;

	}


} // END Class
