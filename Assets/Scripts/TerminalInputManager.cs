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
    public float[] _maxRightAngles = { 90.0f, 90.0f, 0.0f };   
	
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

    //private joint id
   //private zielwinkel

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
            //MoveArm(...)
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

    private void MoveArm(float[] desiredAngles, int firstJointIndex, int lastJointIndex)
    {
        Debug.Log("Aktueller Winkel" + _robotController.jointValues[0]);
        Debug.Log("Kürzester Rot" + CalcShortestRot(_robotController.jointValues[0], desiredAngles[0]));
        int index = 0;
        for (int jointIndex = firstJointIndex; jointIndex <= lastJointIndex; jointIndex++)
        {
            _robotController.jointValues[jointIndex] = Mathf.LerpAngle(_robotController.jointValues[jointIndex], desiredAngles[jointIndex], _timer); 
            index++;
            
            
        }
        _timer += _RobotMovementSpeedModifier * Time.deltaTime;

    }

    // If the return value is positive, then rotate to the left. Else,
    // rotate to the right.
    float CalcShortestRot(float from, float to)
    {
        // If from or to is a negative, we have to recalculate them.
        // For an example, if from = -45 then from(-45) + 360 = 315.
        if (from < 0)
        {
            from += 360;
        }

        if (to < 0)
        {
            to += 360;
        }

        // Do not rotate if from == to.
        if (from == to ||
           from == 0 && to == 360 ||
           from == 360 && to == 0)
        {
            return 0;
        }

        // Pre-calculate left and right.
        float left = (360 - from) + to;
        float right = from - to;
        // If from < to, re-calculate left and right.
        if (from < to)
        {
            if (to > 0)
            {
                left = to - from;
                right = (360 - to) + from;
            }
            else
            {
                left = (360 - to) + from;
                right = to - from;
            }
        }

        // Determine the shortest direction.
        return ((left <= right) ? left : (right * -1));
    }

    // Call CalcShortestRot and check its return value.
    // If CalcShortestRot returns a positive value, then this function
    // will return true for left. Else, false for right.
    bool CalcShortestRotDirection(float from, float to)
    {
        // If the value is positive, return true (left).
        if (CalcShortestRot(from, to) >= 0)
        {
            return true;
        }
        return false; // right
    }


}
