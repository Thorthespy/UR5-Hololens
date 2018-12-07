using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class FWDButton : TerminalButton {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnInputClicked(InputClickedEventData eventData)
    {

        eventData.Use();
    }


    public override void OnInputDown(InputEventData eventData)
    {
        _image.color = _pressedColor;
        _terminalInputManager.ForwardButtonIsPressed = true;
        eventData.Use();
    }

    public override void OnInputUp(InputEventData eventData)
    {
        _image.color = _focusColor;
        _terminalInputManager.ForwardButtonIsPressed = false;
        eventData.Use();
    }
}
