using UnityEngine;
using System.Collections;

public class Player_Move : MonoBehaviour
{

    float walkSpeed = 5.0f;
    float runMultiplier = 2.0f;
    float jumpSpeed = 1000.0f;

    bool isGrounded = true;

    Rigidbody myRigidBody;
    Camera myCamera;

    void Awake()
    {
        myRigidBody = this.GetComponent<Rigidbody>();
        myCamera = this.GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        Rotate();

    }

    void Jump()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, -transform.up, out rayHit, this.transform.localScale.y / 2))
        { isGrounded = true; }
        else
        { isGrounded = false; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(isGrounded);
            if (isGrounded)
            {
                isGrounded = false;
                myRigidBody.AddForce(Vector3.up * jumpSpeed);
            }
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3();
        if (Input.GetAxisRaw("Vertical") > 0) { movement += myRigidBody.transform.forward; }
        if (Input.GetAxisRaw("Vertical") < 0) { movement -= myRigidBody.transform.forward; }
        if (Input.GetAxisRaw("Horizontal") > 0) { movement += myRigidBody.transform.right; }
        if (Input.GetAxisRaw("Horizontal") < 0) { movement -= myRigidBody.transform.right; }


        movement = movement.normalized * walkSpeed * Time.deltaTime;
        //if (Input.GetKeyDown(KeyCode.LeftShift)) { movement *= runMultiplier; }
        if (Input.GetAxisRaw("Vertical") > 0 && Input.GetKey(KeyCode.LeftShift)) { movement *= runMultiplier; }


        myRigidBody.MovePosition(myRigidBody.transform.position + movement);
    }


    void Rotate()
    {
        Vector3 rotation = new Vector3();
        rotation.x = Input.GetAxisRaw("Mouse X");
        rotation.y = Input.GetAxisRaw("Mouse Y");



    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
