using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The program manager controls all the programs and is able to read them. 
public class TerminalProgramManager : MonoBehaviour {

    private Program _program;
    private TerminalInputManager _terminalInputManager;
    [SerializeField]
    private float _threshold = 1.0f;

    private bool _programIsPlaying = false;
    private int _waypointindex;

    public bool ProgramIsPlaying {
        get {
            return _programIsPlaying;
        }

        set {
            _programIsPlaying = value;
        }
    }

    void Start () {
        _program = new Program();
        _terminalInputManager = FindObjectOfType<TerminalInputManager>();
	}
	
    //If a program is playing, check if the Joints are in the correct positions. If they are, reset the timer from the manager and
    //head over to the next waypoint. If this was the last waypoint, stop the program. 
	void Update () {
        if (ProgramIsPlaying)
        {

            if (ArmInCorrecctPosition(_program.WayPoints[_waypointindex].JointAngles, _terminalInputManager.RobotController.jointValues))
            {
                if (_waypointindex < _program.WayPoints.Count-1)
                {
                    _terminalInputManager.ResetTimer();
                    _waypointindex++;
                }  
                else
                {
                    StopProgram();
                }

            }

            _terminalInputManager.MoveArm(_program.WayPoints[_waypointindex].JointAngles);
        }
 
    }

    //Check if the Joints are in the desired angles.
    private bool ArmInCorrecctPosition(float[] aimedAngles, float[] actualAngles)
    {
        for(int index = 0; index < aimedAngles.Length; index++)
        {
            if(!(actualAngles[index] >= aimedAngles[index] - _threshold && actualAngles[index] <= aimedAngles[index] + _threshold))
                return false;
        }
        return true;
        
    }

    private void StopProgram()
    {
        ProgramIsPlaying = false;
        _waypointindex = 0;
        _terminalInputManager.ResetTimer();
    }

    /*public void AddProgram()
    {
        Program program = new Program();
        _programs.Add(program);
    }*/

    //Add a waypoint to the program with the actual angles.
    public void AddWaypoint()
    {
        WayPoint wayPoint = new WayPoint(_terminalInputManager.RobotController.jointValues);
        _program.AddWaypoint(wayPoint);
    }

    /*
    public void RemoveWaypoint()
    {
        _program.RemoveWaypoint();
    } */


    public void RemoveAllWaypoints()
    {
        if(_program != null)
            _program.WayPoints.Clear();
    }

    /*public void RemoveProgram(Program program)
    {
        _programs.Remove(program);
    }*/

    public void PlayProgram()
    {
        if(_program.WayPoints != null && _program.WayPoints.Count > 0)
            ProgramIsPlaying = true;       
    }

    
}
