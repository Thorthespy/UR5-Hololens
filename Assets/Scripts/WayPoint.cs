using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The WayPoint Class is stores all angles for the different joints of the UR5 Robot in a float array. 
public class WayPoint {

    private float[] _jointAngles;

    public WayPoint(float [] angles)
    {
        _jointAngles = new float[angles.Length];
        for (int i = 0; i < angles.Length; i++)
            _jointAngles[i] = angles[i];
    }

    public float[] JointAngles {
        get {
            return _jointAngles;
        }
    }
}
