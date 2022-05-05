using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAudioAndAnimation : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _default, _pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.sprite = _pressed;
        source.PlayOneShot(clip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.sprite = _default;
    }
}
