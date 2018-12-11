using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;

public abstract class AbstractButton : MonoBehaviour, IFocusable, IInputClickHandler, IInputHandler
{

    protected Image _image;
    public Color _focusColor;
    public Color _pressedColor;
    protected Color _originalColor;

    // Use this for initialization
    protected virtual void Start()
    {
        _image = GetComponentInChildren<Image>();
        _focusColor = Color.yellow;
        _pressedColor = Color.green;
        _originalColor = _image.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnFocusEnter()
    {
        _image.color = _focusColor;
    }
    
    public void OnFocusExit()
    {
        _image.color = _originalColor; 
    }

    public abstract void OnInputDown(InputEventData eventData);

    public abstract void OnInputUp(InputEventData eventData);

    public abstract void OnInputClicked(InputClickedEventData eventData);


}
