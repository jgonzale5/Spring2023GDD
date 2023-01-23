using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //The target of the camera
    public Transform Target;
    //How fast (in degrees) does the camera rotate
    public float rotSpeed;

    public float minXAngle;
    public float maxXAngle;

    private bool rotating;
    private Vector3 mousePos;
    private GameObject pivot;
    private float EulerX;

    // Start is called before the first frame update
    void Start()
    {
        //We initialize mousePos to be the mouse position so its not set to (0,0)
        mousePos = Input.mousePosition;

        if (pivot == null)
        {
            pivot = new GameObject("CameraPivot");
            pivot.transform.position = Target.position;
            EulerX = pivot.transform.localEulerAngles.x;  
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
            rotating = true;
        else if (Input.GetButtonUp("Fire2"))
            rotating = false;

        if (rotating)
        {
            Vector3 mouseMovement = Input.mousePosition - mousePos;

            pivot.transform.position = Target.position;
            this.transform.parent = pivot.transform;

            //Rotate around the local up vector of the pivot
            //By the speed we set up, in the direction the mouse is moving
            pivot.transform.Rotate(pivot.transform.up, rotSpeed * Time.deltaTime * mouseMovement.x, Space.Self);

            //Obsolete, since it allowed us to rotate fully around the object
            //pivot.transform.Rotate(Vector3.right, rotSpeed * Time.deltaTime * -mouseMovement.y);

            //We add the rotation to the internally-tracked euler X. Since it's just a float, it won't be 
            //subjected to conversion between euler angles and quaternions, meaning that, for instance,
            //"-90" will never be interpreted as "270"
            EulerX = EulerX + (rotSpeed * Time.deltaTime * -mouseMovement.y);


            //Z is set to 0 so the object is always upright
            pivot.transform.localRotation = Quaternion.Euler(
                Mathf.Clamp(EulerX, minXAngle, maxXAngle),
                pivot.transform.localEulerAngles.y,
                0);

            //pivot.transform.localEulerAngles = new Vector3(
            //    Mathf.Clamp(pivot.transform.localEulerAngles.x, -90, 90),
            //    pivot.transform.localEulerAngles.y,
            //    pivot.transform.localEulerAngles.z);
        }

        //We update mousepos so next frame we can use it
        mousePos = Input.mousePosition;
    }
}
