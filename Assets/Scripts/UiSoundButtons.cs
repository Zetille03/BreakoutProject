using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSoundButtons : MonoBehaviour
{
    [SerializeField] private Sprite offsprite; 
    
    private Sprite _originalSprite; 
    private bool _isPressed = false; 
    private Image _image; 

    private void Awake()
    {
        _image = GetComponent<Image>();
        _originalSprite = _image.sprite; 
    }

    private void Start()
    {
       
        GetComponent<Button>().onClick.AddListener(ClickEvent);
    }

    private void ClickEvent()
    {
        _isPressed = !_isPressed; 
        UpdateSprite(); 
    }

    private void UpdateSprite()
    {
        _image.sprite = _isPressed ? offsprite : _originalSprite;
    }
}
