using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
