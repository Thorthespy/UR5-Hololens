using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;

//This class extends from different InputHandlers. It is for all buttons in the future for the project.
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

    //If the player looks with the cursor on the object, the color will change to the focus color.
    public void OnFocusEnter()
    {
        _image.color = _focusColor;
    }
    
    //If the player looks away, the button will change to it's original color.
    public void OnFocusExit()
    {
        _image.color = _originalColor; 
    }
    
    //This function will be executed if the player clicks and holds a button.
    public abstract void OnInputDown(InputEventData eventData);

    //This function will be executed if the player releases a button.
    public abstract void OnInputUp(InputEventData eventData);

    //This function will be executed if the player just clicks one time on the button.
    public abstract void OnInputClicked(InputClickedEventData eventData);


}
