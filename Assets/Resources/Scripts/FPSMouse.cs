using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FPSMouse : NetworkBehaviour {

    public enum ROTATIONAXIS { MouseX = 1, MouseY = 2}
    public ROTATIONAXIS rotation = ROTATIONAXIS.MouseX | ROTATIONAXIS.MouseY;

    public float sensitivityX = 400f;
    public float minX = -360f;
    public float maxX = 360f;
    private float rotationX = 0f;

    public float sensitivityY = 400f;
    public float minY = -90f;
    public float maxY = 90f;
    private float rotationY = 0f;

    public Quaternion originalRotation;

	void Start ()
    {

        Cursor.lockState = CursorLockMode.Locked;
        
        originalRotation = transform.localRotation;

	} // END Start
	
	
	void Update () 
    {
        if (!isLocalPlayer) return;

        if (rotation == ROTATIONAXIS.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
            rotationX = ClampAngle(rotationX, minX, maxX);
            Quaternion xQuarternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuarternion;
        }


        if (rotation == ROTATIONAXIS.MouseY)
        {
            rotationY -= Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
            rotationY = ClampAngle(rotationY, minY, maxY);
            Quaternion yQuarternion = Quaternion.AngleAxis(rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuarternion;
        }



	} // END Update


    public static float ClampAngle(float angle, float min, float max)
    {

        if (angle < -360)
        { angle += 360; }

        if (angle > 360)
        { angle -= 360; }


        return Mathf.Clamp(angle, min, max);

    } // END ClampAngle


} // END Class
