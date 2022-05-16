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
        if (GetComponent<Button>().interactable)
        {
            _image.sprite = _pressed;
            if (clip != null)
                source.PlayOneShot(clip);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
            _image.sprite = _default;
    }
}
