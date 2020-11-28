using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    float speed = 5f;
    public float timeLerp = 0.1f;
    public GameObject myWeapon;
    public Camera myCamera;
    IController myController;

    public float mouseSensivity;

    private void Start()
    {
        /*if (Application.platform == RuntimePlatform.Android)
            myController =  Main.Instance.mobilePad;
        else
            myController = new ComputerController();*/
        myController = new ComputerController();
    }

    void FixedUpdate()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        //transform.position += (transform.forward * myController.VerticalSpeed() + transform.right * myController.HorizontalSpeed()) * speed * Time.deltaTime;

        float mouseX = myController.HorizontalCameraSpeed() * mouseSensivity;
        float mouseY = myController.VerticalCameraSpeed() * mouseSensivity;

        Vector3 rotateBodyVector3 = transform.rotation.eulerAngles;
        rotateBodyVector3.y += mouseX;


        transform.rotation = Quaternion.Euler(rotateBodyVector3);
    }
}

