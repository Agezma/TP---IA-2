using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VolumeController : MonoBehaviour, IPointerEnterHandler
{
    public Slider slider;
    public Image image;
    public AudioSource[] audioS;
    
    void Start()
    {
        audioS = FindObjectsOfType<AudioSource>();
    }

    public void ChangeVolume()
    {
        for (int i = 0; i < audioS.Length; i++)
        {
            audioS[i].volume = slider.value;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slider.gameObject.SetActive(true);
        image.gameObject.SetActive(false);
    }
}
