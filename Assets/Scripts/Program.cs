using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void AddWaypoint(WayPoint waypoint)
    {
        _wayPoints.Add(waypoint);
    }

    public void RemoveWaypoint(WayPoint transform)
    {
        _wayPoints.Remove(transform);
    }
    
}
