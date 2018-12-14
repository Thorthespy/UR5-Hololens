﻿using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Terminal Input Manager handles all the input from the user if he interacts with the terminal UI.
public class TerminalInputManager : MonoBehaviour, IHoldHandler, IInputHandler
{
    //All the angles for the UR5 Joints.
    [SerializeField]
    private float _RobotMovementSpeedModifier = 0.25f;
    [SerializeField]
    private float[] _maxUpwardAngle = { 0.0f, 0.0f };
    [SerializeField]
    private float[] _maxDownwardAngles = { -90.0f, 160.0f, 180.0f };
    [SerializeField]
    private float[] _maxForwardAngles = { 0.0f, 90.0f, 0.0f };
    [SerializeField]
    private float[] _maxReverseAngles = { 0.0f, -90.0f, 0.0f };
    [SerializeField]
    private float[] _maxLeftAngles = { 90.0f, -90.0f, 0.0f };
    [SerializeField]
    private float[] _maxRightAngles = { 90.0f, 90.0f, 0.0f };   
	
    private bool _upButtonIsPressed = false;
    private bool _downButtonIsPressed = false;
    private bool _forwardButtonIsPressed = false;
    private bool _reverseButtonIsPressed = false;
    private bool _leftButtonIsPressed = false;
    private bool _rightButtonIsPressed = false;
    private bool _jointSliderButtonIsPressed = false;
    private TerminalButton[] _terminalButtons;
    private UR5Controller _robotController;
	private float _timer = 0.0f;
    private SliderUIManager _slideruIManager;
    private TerminalProgramManager _terminalProgramManager;

    private float _jointSliderAngle;
    private int _jointSliderJointIndex;

    public bool UpButtonIsPressed {
        get {
            return _upButtonIsPressed;
        }

        set {
            _upButtonIsPressed = value;
        }
    }

    public bool DownButtonIsPressed {
        get {
            return _downButtonIsPressed;
        }

        set {
            _downButtonIsPressed = value;
        }
    }

    public bool ForwardButtonIsPressed {
        get {
            return _forwardButtonIsPressed;
        }

        set {
            _forwardButtonIsPressed = value;
        }
    }


    public bool RightButtonIsPressed {
        get {
            return _rightButtonIsPressed;
        }

        set {
            _rightButtonIsPressed = value;
        }
    }

    public bool LeftButtonIsPressed {
        get {
            return _leftButtonIsPressed;
        }

        set {
            _leftButtonIsPressed = value;
        }
    }

    public bool ReverseButtonIsPressed {
        get {
            return _reverseButtonIsPressed;
        }

        set {
            _reverseButtonIsPressed = value;
        }
    }

    public bool JointSliderButtonIsPressed {
        get {
            return _jointSliderButtonIsPressed;
        }

        set {
            _jointSliderButtonIsPressed = value;
        }
    }

    public UR5Controller RobotController {
        get {
            return _robotController;
        }
    }

    //This functions sets the joint we are currently moving (for controlling a single joint).
    internal void SetJointSliderButtonInformation(int jointIndex, float aimedAngle)
    {
        _jointSliderJointIndex = jointIndex;
        _jointSliderAngle = aimedAngle;
    }


    //Set Up: Search for important Objects, Notify all Buttons and assign them this gameobject.
    void Start () {
        _terminalButtons = GetComponentsInChildren<TerminalButton>();
        _robotController = FindObjectOfType<UR5Controller>();
        NotifyChildren();
        InputManager.Instance.PushFallbackInputHandler(gameObject);
        _slideruIManager = FindObjectOfType<SliderUIManager>();
        _terminalProgramManager = GetComponent<TerminalProgramManager>();
        for(int i=0; i<RobotController.jointValues.Length; i++)
        {
            _slideruIManager.SetValue(i, RobotController.jointValues[i]);
        }
    }

    //Set the TerminalInputManager to all buttons.
    private void NotifyChildren()
    {
        foreach (TerminalButton Tb in _terminalButtons)
        {
            Tb.AssignTerminalInputManager(this);
        }
    }
	
	//Handle the Right Button. If a programm is playing, the buttons will do nothing.
	void Update () {
        if (_terminalProgramManager.ProgramIsPlaying)
            return;

        if (UpButtonIsPressed)
        {
            MoveArm(_maxUpwardAngle, 1, 2);
        }
        else if (DownButtonIsPressed)
        {
            MoveArm(_maxDownwardAngles, 1, 2);
        }
        else if (ForwardButtonIsPressed)
        {
            MoveArm(_maxForwardAngles, 0, 2);
        }
        else if (ReverseButtonIsPressed)
        {
            MoveArm(_maxReverseAngles, 0, 2);
        }
        else if (LeftButtonIsPressed)
        {
            MoveArm(_maxLeftAngles, 0, 2);
        }
        else if (RightButtonIsPressed)
        {
            MoveArm(_maxRightAngles, 0, 2);
        }
        else if (JointSliderButtonIsPressed)
        {
            MoveArm(_jointSliderAngle, _jointSliderJointIndex);
        }
        else
        {
            ResetTimer();
        }

	}

    public void PlayProgramm()
    {
        _terminalProgramManager.PlayProgram();
    }

    public void AddWaypoint()
    {
        _terminalProgramManager.AddWaypoint();
    }

    public void RemoveWaypoint()
    {
        _terminalProgramManager.RemoveAllWaypoints();
    }

    public void OnHoldStarted(HoldEventData eventData)
    {
        
    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        DeactivateAllButtons();
    }

    private void DeactivateAllButtons()
    {
        DownButtonIsPressed = false;
        UpButtonIsPressed = false;
        ForwardButtonIsPressed = false;
        ReverseButtonIsPressed = false;
        LeftButtonIsPressed = false;
        RightButtonIsPressed = false;
        JointSliderButtonIsPressed = false;
    }

    public void OnHoldCanceled(HoldEventData eventData)
    {
        DeactivateAllButtons();
    }

    //Moves the arm to the desired position: Go through all joints in the robot and lerp from the actual to the desired angle modified
    //by the robot speed. Set the correct value to the correct slider. This function is for moving more than 1 angle at the time.
    private void MoveArm(float[] aimedAngles, int firstJointIndex, int lastJointIndex)
    {
        int index = 0;
        for (int jointIndex = firstJointIndex; jointIndex <= lastJointIndex; jointIndex++)
        {
            RobotController.jointValues[jointIndex] = Mathf.LerpAngle(RobotController.jointValues[jointIndex], aimedAngles[index], _timer);
            _slideruIManager.SetValue(jointIndex, RobotController.jointValues[jointIndex]);
            index++;           
        }
        _timer += _RobotMovementSpeedModifier * Time.deltaTime;
    }

    //Moves the arm the function function before, but this time only for one angle.
    private void MoveArm(float aimedAngle, int jointIndex)
    {
        RobotController.jointValues[jointIndex] = Mathf.Lerp(RobotController.jointValues[jointIndex], aimedAngle, _timer);
        _slideruIManager.SetValue(jointIndex, RobotController.jointValues[jointIndex]);
        _timer += _RobotMovementSpeedModifier * Time.deltaTime;
    }

    //Moves the arm like the function before, but this time for all angles.
    internal void MoveArm(float[] jointAngles)
    {
        for (int jointIndex = 0; jointIndex < jointAngles.Length; jointIndex++)
        {
            RobotController.jointValues[jointIndex] = Mathf.LerpAngle(RobotController.jointValues[jointIndex], jointAngles[jointIndex], _timer);
            _slideruIManager.SetValue(jointIndex, RobotController.jointValues[jointIndex]);
        }
        _timer += _RobotMovementSpeedModifier * Time.deltaTime;
    }

    public void ResetTimer()
    {
        _timer = 0.0f;
    }


    public void OnInputDown(InputEventData eventData)
    {

    }

    public void OnInputUp(InputEventData eventData)
    {
        DeactivateAllButtons();
    }
}
