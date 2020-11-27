using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickDetect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    Button asd;

    public bool touchDown = false;
    public bool touchStay = false;
    public bool touchReleased = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(ButtonPressed(true));
        touchStay = true;
    }   

    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(ButtonReleased(true));
        touchStay = false;
    }

    IEnumerator ButtonPressed(bool newState)
    {
        touchDown = newState;
        yield return new WaitForEndOfFrame();
        touchDown = !newState;
    }
    IEnumerator ButtonReleased(bool newState)
    {
        touchReleased = newState;
        yield return new WaitForEndOfFrame();
        touchReleased = !newState;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
