using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Button _button;
    private TextMeshProUGUI _text; 
    private RectTransform _rectTransform;
    [SerializeField] private Color selectedColor = Color.red; 
    [SerializeField] private Color normalColor = Color.white; 

    private void Awake()
    {
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        _button.onClick.AddListener(ChangeTextColor);
        
    }

    void ChangeTextColor()
    {
        _text.color = selectedColor;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = selectedColor;
        _rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = normalColor;
        _rectTransform.localScale = new Vector3(1, 1, 1);
    }
}
