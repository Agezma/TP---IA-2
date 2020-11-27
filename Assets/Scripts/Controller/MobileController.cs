using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileController : MonoBehaviour, IController
{
    [SerializeField] Joystick joy;
    [SerializeField] Joystick cameraJoy;
    [SerializeField] ClickDetect attackButton;
    [SerializeField] StartRoundMessage message;
    [SerializeField] ClickDetect console;
    
    public float HorizontalSpeed()
    {
        return joy.Horizontal();
    }

    public float VerticalSpeed()
    {
        return joy.Vertical();
    }


    public bool AttackStart()
    {
        return attackButton.touchStay;
    }
   
    public bool AttackRelease()
    {
        return attackButton.touchReleased;
    }

    public bool isAttacking()
    {
        return attackButton.touchStay;
    }

    public float HorizontalCameraSpeed()
    {
        return cameraJoy.Horizontal();
    }

    public float VerticalCameraSpeed()
    {
        return cameraJoy.Vertical();
    }

    public bool SpawnWave()
    {
        return message.touchDown;
    }

    public bool OpenConsole()
    {
        return console.touchDown;
    }
}
