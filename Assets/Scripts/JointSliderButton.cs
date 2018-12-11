using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class JointSliderButton : TerminalButton {

    public Slider _slider;
    [SerializeField]
    private Direction ArrrowDirection;
    [SerializeField]
    private int _jointIndex;
    private float _minValue = -180.0f;
    private float _maxValue = 180.0f;
    private float _aimedAngle;

    private enum Direction { LEFT, RIGHT};

    public override void OnInputClicked(InputClickedEventData eventData)
    {

        eventData.Use();
    }


    public override void OnInputDown(InputEventData eventData)
    {
        _image.color = _pressedColor;
        _terminalInputManager.SetJointSliderButtonInformation(_jointIndex, _aimedAngle);
        _terminalInputManager.JointSliderButtonIsPressed = true;
        eventData.Use();
    }

    public override void OnInputUp(InputEventData eventData)
    {
        _image.color = _focusColor;      
        _terminalInputManager.JointSliderButtonIsPressed = false;
        //terminal input manager button is pressed
        //terminal input manager set id
        //terminal input manager set direction

        // => Ein Tolles Object und nur das Object aber da steigt bestimmt niemand durch z.B. termoinal set new ButtonIfnormation(id, direction);
        eventData.Use();
    }

    protected override void Start()
    {
        base.Start();
        if (this.ArrrowDirection == Direction.LEFT)
            _aimedAngle = _minValue;
        else if(this.ArrrowDirection == Direction.RIGHT)
            _aimedAngle = _maxValue;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
