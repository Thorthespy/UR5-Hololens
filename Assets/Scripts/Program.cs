using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class has all the waypoints for a programm that you can play if you hit the play button. For the future, use this class
//to create more programs.
public class Program {

    private List<WayPoint> _wayPoints;

    public Program ()
    {
        WayPoints = new List<WayPoint>();
    }

    public List<WayPoint> WayPoints {
        get {
            return _wayPoints;
        }

        set {
            _wayPoints = value;
        }
    }

    //Add a Waypoint to the Program
    public void AddWaypoint(WayPoint waypoint)
    {
        _wayPoints.Add(waypoint);
    }

    //Remove a Waypoint from the Program
    public void RemoveWaypoint(WayPoint transform)
    {
        _wayPoints.Remove(transform);
    }
    
}
