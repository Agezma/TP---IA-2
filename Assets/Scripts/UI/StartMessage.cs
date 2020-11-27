using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMessage : MonoBehaviour,IUpdate
{
    public Text msg;
    public IController controller;
       
    void Start()
    {
        UpdateManager.Instance.AddUpdate(this);
        controller = Main.Instance.mobilePad;
    }

    public void OnUpdate()
    {
        if (controller.SpawnWave())
        {
            msg.gameObject.SetActive(false);
            msg.text = "";
        }
    }

}
