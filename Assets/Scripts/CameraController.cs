using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

    public float mouseSensi;
    public float maxYAngle = 70f;
    public bool reset;

    private Vector2 currentRotation;
    private Vector3 baseRotation;

    private Vector3 speedRotation;
    private Vector2 lastRotation;

    private Vector2 lastMouse;

    float xRot, yRot;

    void Start()
    {
        baseRotation = transform.rotation.eulerAngles;
        reset = false;

        lastRotation = transform.eulerAngles;
    }

    void Update()
    {
        //Rotate when LeftClick Hold
        if (Input.GetMouseButton(0) && !reset)
        {
            //float xRot, yRot;

            lastMouse = new Vector2(Input.GetAxis("Mouse X") * mouseSensi, Input.GetAxis("Mouse Y") * mouseSensi);
            //Set currentRotation
            currentRotation.x -= Input.GetAxis("Mouse X") * mouseSensi;
            currentRotation.y += Input.GetAxis("Mouse Y") * mouseSensi;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

            xRot = currentRotation.y;
            yRot = currentRotation.x;

            //transform.rotation = Quaternion.Euler(new Vector3(xRot, yRot, 0) + baseRotation);
        }
        else
        {


            currentRotation.x -= lastMouse.x;
            currentRotation.y += lastMouse.y;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

            xRot = currentRotation.y;
            yRot = currentRotation.x;

            lastMouse *= .9f;

            /*currentRotation = currentRotation * .9f;
            xRot += currentRotation.y;
            yRot += currentRotation.x;*/
        }
        //transform.Rotate(speedRotation, Space.Self);

        transform.rotation = Quaternion.Euler(new Vector3(xRot, yRot, 0) + baseRotation);

        lastRotation = currentRotation;

        /*speedRotation = (transform.eulerAngles - lastRotation);
        speedRotation = new Vector3(speedRotation.x, speedRotation.y, 0);

        lastRotation = transform.eulerAngles;*/
    }
}
