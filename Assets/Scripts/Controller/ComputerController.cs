using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController: IController
{
    public float HorizontalSpeed()
    {
        return Input.GetAxis("Horizontal");
    }
    public float VerticalSpeed()
    {
        return Input.GetAxis("Vertical");
    }
    public bool AttackStart()
    {
        return Input.GetMouseButtonDown(0);
    }
    public bool AttackRelease()
    {
        return Input.GetMouseButtonUp(0);
    }
    public bool isAttacking()
    {
        return Input.GetMouseButton(0);
    }

    public float HorizontalCameraSpeed()
    {
        return Input.GetAxis("Mouse X");
    }

    public float VerticalCameraSpeed()
    {
        return Input.GetAxis("Mouse Y");
    }

    public bool SpawnWave()
    {
        return Input.GetKeyDown(KeyCode.LeftControl);
    }

    public bool OpenConsole()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
