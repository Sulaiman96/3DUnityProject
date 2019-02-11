using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //making these public so I can play around with them in unity.
    #region Variables
    //what the camera will be following
    public Transform target;

    //the camera position, sensitivty and clamp min/max positions the player can look
    public float sensitivity = 2;
    public Vector2 lookMinMax = new Vector2(-50, 50);
    public Vector3 offset;
    public float transitionTime = 0.2f; //this variable will be used to get from point A to B to get to the offset Position

    //Field of View
    public float normalViewAngle = 70;
    public float aimViewAngle = 30;
    public float aimAdjustmentTime = 0.05f; //the speed at which the camera will zoom in when the player aims.

    //wall collisions (for the camera to adjust itself
    public float minDist = 0f;
    private float maxDist;
    public float smooth = 0.1f; //how smooth it will run up againsta  wall
    Vector3 currentPosition;
    private float zDistance;

    //variables for the mouse inputs that have been set up in unity.
    private float mouseX, mouseY, look, turn, finalX, finalY;
    public bool overideCamera;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.localPosition.normalized; //always facing on global axis
        zDistance = transform.localPosition.magnitude;
        maxDist = offset.z;
    }

    private void LateUpdate()
    {
        CameraCollisionChecker();
        InputSettings();
        Rotation();
        OtherInput();
    }

    #region Functions
    private void OtherInput()
    {
        if (Input.GetButton("Aim"))
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, aimViewAngle, aimAdjustmentTime);
            Time.timeScale = 0.7f;
        }
        else if(GetComponent<Camera>().fieldOfView != normalViewAngle)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normalViewAngle, aimAdjustmentTime);
            Time.timeScale = 1f;
        }
    }

    private void Rotation()
    {
        Quaternion newRotation = Quaternion.Euler(finalX, finalY, 0); //variables received from InputSettings 
        Quaternion playerRotation = Quaternion.Euler(0, finalY, 0); //variable received from InputSettings

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetButton("Aim"))
        {
            target.parent.GetComponent<Transform>().rotation = Quaternion.Slerp(target.parent.GetComponent<Transform>().rotation, playerRotation, transitionTime);
        }

        target.rotation = newRotation;
        transform.position = Vector3.Lerp(transform.position, target.transform.position - (newRotation * offset), transitionTime);
    }

    private void InputSettings()
    {
        //looking
        mouseX -= Input.GetAxis("Mouse Y");
        mouseX = Mathf.Clamp(mouseX, lookMinMax.x, lookMinMax.y);
        look -= Input.GetAxis("Look");
        look = Mathf.Clamp(look, lookMinMax.x, lookMinMax.y);

        //turning
        mouseY += Input.GetAxis("Mouse X");
        turn += Input.GetAxis("Turn");

        finalX = (mouseX + look) * sensitivity;
        finalY = (mouseY + turn) * sensitivity;
    }

    private void CameraCollisionChecker()
    {
        Vector3 cameraPosition = transform.parent.TransformPoint(currentPosition * maxDist);
        RaycastHit hit;

        if(Physics.Linecast(transform.parent.position, cameraPosition, out hit)) 
        {
            zDistance = Mathf.Clamp((hit.distance * 0.9f), minDist, maxDist);
        }
        else
        {
            zDistance = maxDist;
        }
        Vector3 newPosition = new Vector3(offset.x, offset.y, -zDistance);

        transform.localPosition = Vector3.Lerp(newPosition, currentPosition * zDistance, smooth);
    }
    #endregion
}
