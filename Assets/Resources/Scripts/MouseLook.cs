using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{

    public enum RotationAxes { MouseXAndY, MouseX, MouseY }

    public RotationAxes rotateAroundAxes = RotationAxes.MouseXAndY;

    public float sensivityX = 15.0f;
    public float sensivityY = 15.0f;

    public float minX = -360.0f;
    public float maxX = 360.0f;

    public float minY = -60.0f;
    public float maxY = 60.0f;

    Transform myTransform;
    Rigidbody myRigidBody;

    float rotationY = 0.0f;

    void Awake()
    {
        myTransform = this.transform;
        myRigidBody = this.GetComponent<Rigidbody>();

        if (myRigidBody) myRigidBody.freezeRotation = true;
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rotateAroundAxes == RotationAxes.MouseXAndY)
        {
            float rotationX = myTransform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensivityX;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);

            rotationY += Input.GetAxis("Mouse Y") * sensivityY;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            myTransform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (rotateAroundAxes == RotationAxes.MouseX)
        {
            float rotationX = myTransform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensivityX;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);

            myTransform.localEulerAngles = new Vector3(0, rotationX, 0);
        }
        else if (rotateAroundAxes == RotationAxes.MouseY)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensivityY;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            myTransform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }

    }
}
