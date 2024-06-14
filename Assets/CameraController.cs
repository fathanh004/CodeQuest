using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    static CameraController instance;
    public static CameraController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraController>();
            }
            return instance;
        }
    }
    Camera camera => GetComponent<Camera>();
    public float movementSpeed = 10f;
    public float zoomSensitivity = 10f;
    public bool leftButtonPressed = false;
    public bool rightButtonPressed = false;
    public float perspectiveZoomSpeed = 0.5f; // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f; // The rate of change of the orthographic size in orthographic mode.

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || leftButtonPressed)
        {
            MoveCameraToLeft();
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || rightButtonPressed)
        {
            MoveCameraToRight();
        }

        float axis = Input.GetAxis("Mouse ScrollWheel");
        if (axis != 0)
        {
            transform.position = transform.position + transform.forward * axis * zoomSensitivity;
        }
        else
        {
            ZoomPinch();
        }

        ClampCameraPosition();
    }

    public void ClampCameraPosition()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -10f, 10f),
            Mathf.Clamp(transform.position.y, 5f, 15f),
            Mathf.Clamp(transform.position.z, -5f, 15f)
        );
    }

    public void MoveCameraToLeft()
    {
        transform.position =
            transform.position + (-transform.right * movementSpeed * Time.deltaTime);
    }

    public void MoveCameraToRight()
    {
        transform.position =
            transform.position + (transform.right * movementSpeed * Time.deltaTime);
    }

    //zoom using touch input
    public void ZoomPinch()
    {
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (camera.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }
    }
}
