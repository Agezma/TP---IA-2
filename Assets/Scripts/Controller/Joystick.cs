using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private float maxDist = 50;
    [SerializeField] Vector3 stickValue;
    
    private void Start()
    {
        originalPosition = transform.position;
    }
    #region INTERFACES
    public void OnDrag(PointerEventData eventData)
    {
        stickValue = Vector3.ClampMagnitude((Vector3)eventData.position - originalPosition, maxDist);
        this.transform.position = stickValue + originalPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.position = originalPosition;
        stickValue = Vector3.zero;
    }
    #endregion

    public float Horizontal()
    {
        return stickValue.x / maxDist;
    }
    public float Vertical()
    {
        return stickValue.y / maxDist;
    }
}
