using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class VolumeControllerPart2 : MonoBehaviour, IEndDragHandler, IPointerExitHandler, IBeginDragHandler
{
    public Slider slide;
    public Image volume;
    bool isDragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }
       
    public void OnEndDrag(PointerEventData eventData)
    {
        slide.gameObject.SetActive(false);
        volume.gameObject.SetActive(true);
        isDragging = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isDragging)
        {
            slide.gameObject.SetActive(false);
            volume.gameObject.SetActive(true);
        }
    }
}
