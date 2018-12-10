using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class JointSliderButton : TerminalButton {

    public Slider _slider;
    [SerializeField]
    private Direction ArrrowDirection;
    private float _minValue = -180;
    private float _maxValue = 180;
    public int id = 0;
    private enum Direction { LEFT, RIGHT};


    public override void OnInputClicked(InputClickedEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnInputDown(InputEventData eventData)
    {
        throw new System.NotImplementedException();
        // IPM braucht id 
    }

    public override void OnInputUp(InputEventData eventData)
    {
        throw new System.NotImplementedException();
    }


    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
