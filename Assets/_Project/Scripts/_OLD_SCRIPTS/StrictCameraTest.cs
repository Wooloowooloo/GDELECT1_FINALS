using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrictCameraTest : MonoBehaviour
{
    public float speedV = 2.0f;
    public float speedH = 2.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    public bool cursorLocked = true;

    private void Start()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }        
    }

    private void Update()
    {
        rotationY += speedV * Input.GetAxis("Mouse X") * Time.timeScale;
        rotationX -= speedH * Input.GetAxis("Mouse Y") * Time.timeScale;

        rotationX = Mathf.Clamp(rotationX, -20f, 10f);
        rotationY = Mathf.Clamp(rotationY, -30f, 30f);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}