using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInputManager : MonoBehaviour, IHoldHandler
{
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

    internal void SetJointSliderButtonInformation(int jointIndex, float aimedAngle)
    {
        _jointSliderJointIndex = jointIndex;
        _jointSliderAngle = aimedAngle;
    }


    // Use this for initialization
    void Start () {
        _terminalButtons = GetComponentsInChildren<TerminalButton>();
        _robotController = FindObjectOfType<UR5Controller>();
        NotifyChildren();
        InputManager.Instance.PushFallbackInputHandler(gameObject);
        _slideruIManager = FindObjectOfType<SliderUIManager>();
        for(int i=0; i<_robotController.jointValues.Length; i++)
        {
            _slideruIManager.SetValue(i, _robotController.jointValues[i]);
        }
    }

    private void NotifyChildren()
    {
        foreach (TerminalButton Tb in _terminalButtons)
        {
            Tb.AssignTerminalInputManager(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(UpButtonIsPressed)
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
            // Wir brauchen folgendes:
            // Welcher Joint?
            // Welcher Winkel max/min?
            // 
        }
        else
        {
            _timer = 0.0f;
        }

	}

    public void OnHoldStarted(HoldEventData eventData)
    {
        
    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        DownButtonIsPressed = false;
        UpButtonIsPressed = false;
        ForwardButtonIsPressed = false;
        ReverseButtonIsPressed = false;
        LeftButtonIsPressed = false;
        RightButtonIsPressed = false;
    }

    public void OnHoldCanceled(HoldEventData eventData)
    {
        DownButtonIsPressed = false;
        UpButtonIsPressed = false;
        ForwardButtonIsPressed = false;
        ReverseButtonIsPressed = false;
        LeftButtonIsPressed = false;
        RightButtonIsPressed = false;
    }

    private void MoveArm(float[] aimedAngles, int firstJointIndex, int lastJointIndex)
    {
        int index = 0;
        for (int jointIndex = firstJointIndex; jointIndex <= lastJointIndex; jointIndex++)
        {
            _robotController.jointValues[jointIndex] = Mathf.LerpAngle(_robotController.jointValues[jointIndex], aimedAngles[index], _timer);
            _slideruIManager.SetValue(jointIndex, _robotController.jointValues[jointIndex]);
            index++;           
        }
        _timer += _RobotMovementSpeedModifier * Time.deltaTime;
    }

    private void MoveArm(float aimedAngle, int jointIndex)
    {
        _robotController.jointValues[jointIndex] = Mathf.Lerp(_robotController.jointValues[jointIndex], aimedAngle, _timer);
        _slideruIManager.SetValue(jointIndex, _robotController.jointValues[jointIndex]);
        _timer += _RobotMovementSpeedModifier * Time.deltaTime;
    }

}
