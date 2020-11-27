using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMouse : MonoBehaviour, IUpdate
{
    void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            UpdateManager.Instance.AddUpdate(this);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
            LockMouse();
        else if (Input.GetKeyDown(KeyCode.Escape))
            UnlockMouse();
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

}
