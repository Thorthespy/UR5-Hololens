using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInputManager : MonoBehaviour, IHoldHandler
{

    private TerminalButton[] _terminalButtons;
    private UR5Controller _robotController;
    private float _timer = 0.0f;
    public float _forwardAngle = 0.0f;
    private float _maxUpwardAngle = 0.0f;
    public float[] _maxDownwardAngles = { -90.0f, 160.0f, 180.0f };
    public float[] _maxForwardAngles = { 0.0f, 90.0f, 0.0f };
    public float[] _maxReverseAngles = { 0.0f, -90.0f, 0.0f };
    public float[] _maxLeftAngles = { 90.0f, -90.0f, 0.0f };
    public float[] _maxRightAngles = { 90.0f, 90.0f, 0.0f };
    public float _RobotMovementSpeedModifier = 0.25f;
    private bool _upButtonIsPressed = false;
    private bool _downButtonIsPressed = false;
    private bool _forwardButtonIsPressed = false;
    private bool _reverseButtonIsPressed = false;
    private bool _leftButtonIsPressed = false;
    private bool _rightButtonIsPressed = false;

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

    // Use this for initialization
    void Start () {
        _terminalButtons = GetComponentsInChildren<TerminalButton>();
        _robotController = FindObjectOfType<UR5Controller>();
        NotifyChildren();
        InputManager.Instance.PushFallbackInputHandler(gameObject);
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
            MoveArmUpdwards();
        }
        else if (DownButtonIsPressed)
        {
            MoveArmDownWards();
        }
        else if (ForwardButtonIsPressed)
        {
            MoveArmForward();
        }
        else if (ReverseButtonIsPressed)
        {
            MoveArmBackwards();
        }
        else if (LeftButtonIsPressed)
        {
            MoveArmLeft();
        }
        else if (RightButtonIsPressed)
        {
            MoveArmRight();
        }
        else
        {
            _timer = 0.0f;
        }

	}

    private void MoveArmRight()
    {
        float joint0 = _robotController.jointValues[0];
        float joint1 = _robotController.jointValues[1];
        float joint2 = _robotController.jointValues[2];

        _robotController.jointValues[0] = Mathf.Lerp(joint0, _maxRightAngles[0], _timer);
        _robotController.jointValues[1] = Mathf.Lerp(joint1, _maxRightAngles[1], _timer);
        _robotController.jointValues[2] = Mathf.Lerp(joint2, _maxRightAngles[2], _timer);

        _timer += _RobotMovementSpeedModifier * Time.deltaTime;
    }

    private void MoveArmLeft()
    {
        float joint0 = _robotController.jointValues[0];
        float joint1 = _robotController.jointValues[1];
        float joint2 = _robotController.jointValues[2];

        _robotController.jointValues[0] = Mathf.Lerp(joint0, _maxLeftAngles[0], _timer);
        _robotController.jointValues[1] = Mathf.Lerp(joint1, _maxLeftAngles[1], _timer);
        _robotController.jointValues[2] = Mathf.Lerp(joint2, _maxLeftAngles[2], _timer);

        _timer += _RobotMovementSpeedModifier * Time.deltaTime;
    }

    private void MoveArmBackwards()
    {
        float joint0 = _robotController.jointValues[0];
        float joint1 = _robotController.jointValues[1];
        float joint2 = _robotController.jointValues[2];

        _robotController.jointValues[0] = Mathf.Lerp(joint0, _maxReverseAngles[0], _timer);
        _robotController.jointValues[1] = Mathf.Lerp(joint1, _maxReverseAngles[1], _timer);
        _robotController.jointValues[2] = Mathf.Lerp(joint2, _maxReverseAngles[2], _timer);

        _timer += _RobotMovementSpeedModifier * Time.deltaTime;
    }

    private void MoveArmUpdwards()
    {
        float joint1 = _robotController.jointValues[1];
        float joint2 = _robotController.jointValues[2];
        //float joint3 = _robotController.jointValues[3];

        _robotController.jointValues[1] = Mathf.Lerp(joint1, _maxUpwardAngle, _timer);
        _robotController.jointValues[2] = Mathf.Lerp(joint2, _maxUpwardAngle, _timer);
        //_robotController.jointValues[3] = Mathf.Lerp(joint3, _maxUpwardAngle, _timer);

        _timer += _RobotMovementSpeedModifier * Time.deltaTime;
    }

    private void MoveArmDownWards()
    {
        float joint1 = _robotController.jointValues[1];
        float joint2 = _robotController.jointValues[2];
       // float joint3 = _robotController.jointValues[3];

        _robotController.jointValues[1] = Mathf.Lerp(joint1, _maxDownwardAngles[0], _timer);
        _robotController.jointValues[2] = Mathf.Lerp(joint2, _maxDownwardAngles[1], _timer);
        //_robotController.jointValues[3] = Mathf.Lerp(joint3, _maxDownwardAngles[2], _timer);

        _timer += _RobotMovementSpeedModifier * Time.deltaTime;
    }

    private void MoveArmForward()
    {
        float joint0 = _robotController.jointValues[0];
        float joint1 = _robotController.jointValues[1];
        float joint2 = _robotController.jointValues[2];

        _robotController.jointValues[0] = Mathf.Lerp(joint0, _maxForwardAngles[0], _timer);
        _robotController.jointValues[1] = Mathf.Lerp(joint1, _maxForwardAngles[1], _timer);
        _robotController.jointValues[2] = Mathf.Lerp(joint2, _maxForwardAngles[2], _timer);

        _timer += _RobotMovementSpeedModifier * Time.deltaTime;
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
}
