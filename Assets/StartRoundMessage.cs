using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartRoundMessage : MonoBehaviour, IPointerDownHandler
{
    public bool touchDown = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(ButtonPressed(true));
    }
    IEnumerator ButtonPressed(bool newState)
    {
        touchDown = newState;
        yield return new WaitForSeconds(0.05f);
        touchDown = !newState;
    }
}
