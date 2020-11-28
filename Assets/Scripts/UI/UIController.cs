using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour, IUpdate
{
    public RectTransform Console;
    IController controller;

    void Start()
    {
        UpdateManager.Instance.AddUpdate(this);   
        /*if (Application.platform == RuntimePlatform.Android)
        {
            mobilePad.gameObject.SetActive(true);
            controller =  Main.Instance.mobilePad;

        }
        else
        {
            mobilePad.gameObject.SetActive(false);
            controller = new ComputerController();
        }*/
        controller = new ComputerController();
    }

    public void OnUpdate ()
    {
        if(controller.OpenConsole())
        {
            if (!Console.gameObject.activeSelf)
                StartCoroutine(GrowUp());
            else
                StartCoroutine(GetDown());
        }

    }

    IEnumerator GetDown()
    {
        while(Console.localScale.x >=0)
        {
            Console.localScale = new Vector3(Console.transform.localScale.x-0.1f, Console.transform.localScale.y - 0.1f, Console.transform.localScale.z - 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        Console.gameObject.SetActive(false);
    }

    IEnumerator GrowUp()
    {
        Console.gameObject.SetActive(true);
        while (Console.localScale.x <= 0.9f)
        {
            Console.transform.localScale = new Vector3(Console.transform.localScale.x + 0.1f, Console.transform.localScale.y + 0.1f, Console.transform.localScale.z + 0.1f);
            Console.transform.localScale = new Vector3(Mathf.Clamp(Console.transform.localScale.x + 0.1f, 0, 1), Mathf.Clamp(Console.transform.localScale.y + 0.1f, 0, 1), Mathf.Clamp(Console.transform.localScale.z + 0.1f, 0, 1));
            yield return new WaitForSeconds(0.05f);
        }
    }
}
